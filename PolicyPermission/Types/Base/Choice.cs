using System.Reflection;

namespace PolicyPermission.Types.Base
{
    public abstract class Choice<T> : IFormattable where T : Choice<T>
    {
        private string Name { get; }

        protected Choice(string name)
        {
            var allOptions = GetAllOptions();
            if (allOptions.Any(x => x?.Name == name)) throw new DuplicateChoiceException();
            Name = name;
        }

        public static IEnumerable<T> GetAllOptions()
        {
            var type = typeof(T);
            var allStaticFields = type.GetFields(BindingFlags.Static | BindingFlags.Public).Where(x => x.FieldType == type);
            var allStaticProperties = type.GetProperties(BindingFlags.Static | BindingFlags.Public).Where(x => x.PropertyType == type);
            return allStaticFields.Select(x => x.GetValue(null)).Concat(allStaticProperties.Select(x => x.GetValue(null))).Cast<T>();
        }
        
        public static T FromString(string name) => GetAllOptions().FirstOrDefault(x => x.Name == name) ?? throw new InvalidOptionException();
        public override string ToString() => Name;
        public string ToString(string? format, IFormatProvider? formatProvider) => Name;

        public static bool operator ==(Choice<T> left, Choice<T> right) => left.Name == right.Name && left.GetType() == right.GetType();
        public static bool operator !=(Choice<T> left, Choice<T> right) => !(left == right);
        
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not T choice) throw new IncompatibleTypesException();
            return choice == this;
        }
        
        public int CompareTo(object obj)
        {
            if (obj is not T choice) throw new IncompatibleTypesException();
            return string.Compare(Name, choice.Name, StringComparison.Ordinal);
        }

        public override int GetHashCode() => Name.GetHashCode();
    }

    public class DuplicateChoiceException : Exception
    {
        public DuplicateChoiceException(string message="Option already exists!") : base(message)
        {
        }
    }
    
    public class InvalidOptionException : Exception
    {
        public InvalidOptionException(string message="This option does not exist!") : base(message)
        {
        }
    }
    
    public class IncompatibleTypesException : Exception
    {
        public IncompatibleTypesException(string message="These types are not compatible!") : base(message)
        {
        }
    }
}
