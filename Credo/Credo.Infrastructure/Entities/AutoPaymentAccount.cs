using Credo.Infrastructure.Entities.Abstraction;
using Credo.Infrastructure.Enums;
using Timer = System.Timers.Timer;

namespace Credo.Infrastructure.Entities;

public class AutoPaymentAccount : Entity
{
  public int AccountId { get; set; }
  public Account Account { get; set; }
  public string TargetAccountNumber { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public decimal Amount { get; set; }
  public int FrequencyInDays { get; set; }
}