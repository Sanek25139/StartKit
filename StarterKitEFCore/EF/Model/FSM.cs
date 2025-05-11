using Microsoft.EntityFrameworkCore;

namespace StarterKit.EF.Model
{
    [Owned]
    public class FSM
    {
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;

        public FSM() { }
        public FSM(string fsm)
        {
            string[] str = fsm.Split(' ');
            FirstName = str[0];
            SecondName = str[1];
            MiddleName = str[2];
        }
        public string ToFullString() => $"{FirstName} {SecondName} {MiddleName}";

        public string ToShortString() => $"{FirstName} {SecondName[0]}.{MiddleName[0]}.";

        public override string ToString() => ToFullString();

        public bool Contains(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return false;

            return ToFullString().Contains(searchText, StringComparison.CurrentCultureIgnoreCase) ||
                   ToShortString().Contains(searchText, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
