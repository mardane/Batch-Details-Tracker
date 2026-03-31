using System.ComponentModel.DataAnnotations;

namespace BatchDetailsTracker.Web.Models;

public class BatchRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, StringLength(80)]
    public string BatchName { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string StoreName { get; set; } = "Instacart";

    [Required]
    public DateTime BatchDate { get; set; } = DateTime.Today;

    [Required]
    public BatchStatus Status { get; set; } = BatchStatus.Active;

    [Range(1, 10)]
    public int OrdersCount { get; set; } = 1;

    [Range(0, 5000)]
    public decimal BasePay { get; set; }

    [Range(0, 5000)]
    public decimal TipAmount { get; set; }

    [Range(-5000, 5000)]
    public decimal Adjustments { get; set; }

    [Range(0, 500)]
    public double MilesDriven { get; set; }

    [Range(0, 24)]
    public double HoursSpent { get; set; } = 1;

    [StringLength(240)]
    public string Notes { get; set; } = string.Empty;

    public decimal TotalEarnings => BasePay + TipAmount + Adjustments;

    public decimal ProfitPerHour => HoursSpent <= 0 ? 0 : TotalEarnings / (decimal)HoursSpent;

    public decimal ProfitPerOrder => OrdersCount <= 0 ? 0 : TotalEarnings / OrdersCount;
}

public enum BatchStatus
{
    Active,
    Completed,
    Cancelled
}
