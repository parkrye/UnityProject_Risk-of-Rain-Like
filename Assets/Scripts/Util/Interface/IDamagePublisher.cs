public interface IDamagePublisher
{
    public void AddDamageSubscriber(IDamageSubscriber _subscriber);
    public void RemoveDamageSubscriber(IDamageSubscriber _subscriber);
    public float DamageOccurrence(float _damage);
}
