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

        public override string ToString()
        {
            return $"{FirstName} {SecondName} {MiddleName}";
        }
    }
}
