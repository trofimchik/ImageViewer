namespace Common
{
    public static class GeneralErrors
    {
        public static Error ValueIsInvalid(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(name);
            return new Error("value.is.invalid", $"Value is invalid for {name}");
        }

        public static Error ValueIsRequired(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(name);
            return new Error("value.is.required", $"Value is required for {name}");
        }
    }
}
