using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Entities;

public class User(string name, string email, string password)
{
    public int Id { get; set; }
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;

    private readonly List<ObservingBase> _observings = [];
    public IReadOnlyList<ObservingBase> Observings => _observings;

    public void AddObserving(ObservingBase observing)
    {
        _observings.Add(observing);
    }
}