namespace WebObserver.Main.Domain.Base;

public record BeforeAfter<T>(T Before, T After) where T : class;