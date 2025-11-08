using System.Linq;

namespace Domain.Entities;

public class Sale
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string CreationUser { get; set; } = null!;
    public decimal Total { get; set; }
    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();

    public void AddDetail(SalesDetail detail)
    {
        SalesDetails.Add(detail);
        Total = SalesDetails.Sum(sd => sd.Quantity * sd.UnitPrice);
    }
}

