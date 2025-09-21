namespace JWTLoginAuthorizationRoleBased.Model;

public partial class Account
{
    public int AccountId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public decimal Balance { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
