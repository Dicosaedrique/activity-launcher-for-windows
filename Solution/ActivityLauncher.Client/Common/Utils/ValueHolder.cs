using Microsoft.AspNetCore.Components;

namespace ActivityLauncher.Client.Common.Utils;

public class ValueHolder<TValue>
{
    public TValue Value { get; private set; }

    private readonly EventCallback<TValue> _valueChanged;

    public ValueHolder(TValue defaultValue, Action<TValue> action)
    {
        Value = defaultValue;
        _valueChanged = EventCallback.Factory.Create(this, action);
    }

    public async Task SetValue(TValue value)
    {
        Value = value;
        await _valueChanged.InvokeAsync(Value);
    }
}
