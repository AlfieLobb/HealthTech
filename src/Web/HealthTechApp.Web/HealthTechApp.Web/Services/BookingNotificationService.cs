namespace HealthTechApp.Web.Services;

public class BookingNotificationService
{
    // Locking manually because we need multiple values per key, and only need to lock very briefly
    private readonly object _subscriptionsLock = new();
    private readonly Dictionary<string, HashSet<Subscription>> _subscriptionsByUserId = new();

    public IDisposable SubscribeToBookingNotifications(string userId, Func<Task> callback)
    {
        var subscription = new Subscription(this, userId, callback);

        lock (_subscriptionsLock)
        {
            if (!_subscriptionsByUserId.TryGetValue(userId, out var subscriptions))
            {
                subscriptions = [];
                _subscriptionsByUserId.Add(userId, subscriptions);
            }

            subscriptions.Add(subscription);
        }

        return subscription;
    }

    public Task NotifyBookingsStatusChangedAsync()
    {
        lock (_subscriptionsLock)
        {
            return Task.WhenAll(_subscriptionsByUserId.SelectMany(x => x.Value).Select(x => x.NotifyAsync()));
        }
    }

    private void Unsubscribe(string userId, Subscription subscription)
    {
        lock (_subscriptionsLock)
        {
            if (_subscriptionsByUserId.TryGetValue(userId, out var subscriptions))
            {
                subscriptions.Remove(subscription);
                if (subscriptions.Count == 0)
                {
                    _subscriptionsByUserId.Remove(userId);
                }
            }
        }
    }

    private class Subscription(BookingNotificationService owner, string userId, Func<Task> callback) : IDisposable
    {
        public Task NotifyAsync()
        {
            return callback();
        }

        public void Dispose()
            => owner.Unsubscribe(userId, this);
    }
}

