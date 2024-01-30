using System;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using Services.Util;

namespace Services
{
    public class DebtManager : IDebtService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        private List<decimal[]> bestTransactions;
        private Dictionary<decimal, decimal> indexToUserId; // Indeks ile UserID arasındaki ilişkiyi tutacak sözlük

        public DebtManager(IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task CreateDeptAsync(Debt debt) => await _manager.Debt.CreateDeptAsync(debt);

        public async Task DeleteDeptAsync(Debt debt) => await _manager.Debt.DeleteDeptAsync(debt);

        public async Task UpdateDeptAsync(Debt debt) => await _manager.Debt.UpdateDeptAsync(debt);

        public async Task<List<DebtReadDto>> GetDeptsByVacationIDAsync(int ID)
        {
            var debts = await _manager.Debt.GetDeptsByVacationIDAsync(ID);
            var returnList = new List<DebtReadDto>();
            foreach(var debt in debts)
            {
                var obj = new DebtReadDto();
                obj.ID = debt.ID;
                obj.Amount = debt.Amount < 0 ? debt.Amount * -1 : debt.Amount;
                obj.VacationID = debt.VacationID;
                obj.CreditorUser = _mapper.Map<UserInfoDto>(await _manager.User.GetUserByIdAsync(debt.Amount > 0 ? debt.CreditorUserID : debt.DebtorUserID, false));
                obj.DebtorUser = _mapper.Map<UserInfoDto>(await _manager.User.GetUserByIdAsync(debt.Amount > 0 ? debt.DebtorUserID : debt.CreditorUserID, false));
                returnList.Add(obj);
            }
            return returnList;
        }

        public async Task ControlAndSaveDebtAsync(int vacationID, List<PaymentReadDto> payments)
        {
            try
            {
                List<decimal[]> transactions = new List<decimal[]>();
                foreach (var payment in payments)
                {
                    var paidUserID = payment.PaidUser.ID; // Ödeyen Kullanıcı
                                                          //bool isPayerIncluded = false; // ödeyen kişi de harcamaya dahil mi?

                    var amountPerPerson = Math.Round(payment.Price / payment.Participants.Count, 2); // Kişi başı ödenecek miktar
                    foreach (var participant in payment.Participants)
                    {
                        //if (participant.ParticipantUserID.Equals(paidUserID)) {
                        //    isPayerIncluded = true;
                        //   break;
                        // }
                        if (participant.ParticipantUser.ID.Equals(paidUserID)) // kendi kendine borç yazmasın.
                            continue;

                        var transaction = new decimal[] { paidUserID, participant.ParticipantUser.ID, amountPerPerson };
                        transactions.Add(transaction);
                    }

                }
                if (transactions != null && transactions.Count > 0)
                    MinTransfers(transactions);

                
                    var oldDebts = await _manager.Debt.GetDeptsByVacationIDAsync(vacationID);
                    foreach (var oldDebt in oldDebts)
                    {
                        await DeleteDeptAsync(oldDebt);
                        await _manager.SaveAsync();
                    }
               
                    foreach (var transaction in bestTransactions)
                    {
                        int from = Convert.ToInt32(transaction[0]);
                        int to = Convert.ToInt32(transaction[1]);
                        decimal amount = transaction[2];

                        Debt debt = new Debt();
                        debt.Amount = amount;
                        debt.CreditorUserID = to;
                        debt.DebtorUserID = from;
                        debt.VacationID = vacationID;

                        await CreateDeptAsync(debt);
                        await _manager.SaveAsync();

                    }
                
            }
            catch(Exception e)
            {
               // TODO
            }
        }

        public int MinTransfers(List<decimal[]> transactions)
        {
            Dictionary<decimal, decimal> memberVsBalance = new Dictionary<decimal, decimal>();

            // Compute the overall balance (Incoming - Outgoing) for each member
            foreach (decimal[] txn in transactions)
            {
                decimal from = txn[0];
                decimal to = txn[1];
                decimal amount = txn[2];

                memberVsBalance[from] = memberVsBalance.GetValueOrDefault(from, 0) - amount;
                memberVsBalance[to] = memberVsBalance.GetValueOrDefault(to, 0) + amount;
            }

            List<(decimal userId, decimal balance)> balances = new List<(decimal, decimal)>();
            foreach (var entry in memberVsBalance)
            {
                decimal userId = entry.Key;
                decimal balance = entry.Value;
                balances.Add((userId, balance));
            }

            bestTransactions = new List<decimal[]>(); // Yeni liste oluşturuluyor
            FindMinimumTxns(new List<(decimal, decimal)>(balances), 0, new List<decimal[]>());
            Console.WriteLine("Minimum Transaction Count: " + bestTransactions.Count);
            //PrintTransactions();

            return bestTransactions.Count;
        }

        private void FindMinimumTxns(List<(decimal userId, decimal balance)> balances, int currentIndex, List<decimal[]> currentTransactions)
        {
            if (currentIndex >= balances.Count)
            {
                if (currentTransactions.Count > bestTransactions.Count)
                {
                    bestTransactions = new List<decimal[]>(currentTransactions);
                }
                return;
            }

            decimal currentVal = balances[currentIndex].balance;
            if (currentVal == 0)
            {
                FindMinimumTxns(balances, currentIndex + 1, currentTransactions);
                return;
            }

            for (int txnIndex = currentIndex + 1; txnIndex < balances.Count; txnIndex++)
            {
                decimal nextVal = balances[txnIndex].balance;
                if (currentVal * nextVal < 0)
                {
                    balances[txnIndex] = (balances[txnIndex].userId, currentVal + nextVal);

                    decimal fromUserId = balances[currentIndex].userId;
                    decimal toUserId = balances[txnIndex].userId;

                    currentTransactions.Add(new decimal[] { fromUserId, toUserId, Math.Min(currentVal, -nextVal) });

                    FindMinimumTxns(balances, currentIndex + 1, currentTransactions);

                    balances[txnIndex] = (balances[txnIndex].userId, nextVal);
                    currentTransactions.RemoveAt(currentTransactions.Count - 1);

                    if (currentVal + nextVal == 0)
                    {
                        break;
                    }
                }
            }
        }


        public async Task PayDebtAsync(PayDebtDto payDebtDto)
        {
            Payment payment = CreatePayment(payDebtDto);
            await _manager.Payment.CreatePaymentAsync(payment);
            await _manager.SaveAsync();

            PaymentParticipant participant = CreatePaymentParticipant(payment.ID, payDebtDto.CreditorUserID);
            await _manager.PaymentParticipant.CreatePaymentParticipantAsync(participant);
            await _manager.SaveAsync();
        }

        //private void PrintTransactions()
        //{
        //    Console.WriteLine("Optimal Transactions:");

        //    foreach (decimal[] txn in bestTransactions)
        //    {
        //        int from = txn[0];
        //        int to = txn[1];
        //        int amount = txn[2];

        //        Console.WriteLine($"From: {from}, To: {to}, Amount: {amount}");
        //    }
        //}

        //son

        private Payment CreatePayment(PayDebtDto payDebtDto)
        {
            return new Payment
            {
                PaidUserID = payDebtDto.DebtorUserID,
                IsDebt = true,
                VacationID = payDebtDto.VacationID,
                PaidDateTime = DateTime.Now,
                Price = payDebtDto.Amount,
                ProductDescription = string.Empty,
                ProductName = string.Empty
            };
        }

        private PaymentParticipant CreatePaymentParticipant(int paymentId, int creditorUserId)
        {
            return new PaymentParticipant
            {
                PaymentID = paymentId,
                ParticipantUserID = creditorUserId
            };
        }
    }
}

