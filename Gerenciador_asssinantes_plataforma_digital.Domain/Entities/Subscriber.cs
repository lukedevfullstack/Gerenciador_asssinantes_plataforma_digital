using Gerenciador_asssinantes_plataforma_digital.Domain.Enums;

namespace Gerenciador_asssinantes_plataforma_digital.Domain.Entities
{
    public class Subscriber
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime SubscriptionDate { get; private set; }
        public PlanType Plan { get; private set; }
        public decimal MonthlyValue { get; private set; }
        public bool IsActive { get; private set; }

        public int SubscriptionMonths => CalculateMonths();

        protected Subscriber() { }

        public Subscriber(string fullName, string email, DateTime subscriptionDate, decimal monthlyValue, PlanType plan)
        {
            Validate(fullName, email, subscriptionDate, monthlyValue);

            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            SubscriptionDate = subscriptionDate;
            MonthlyValue = monthlyValue;
            Plan = plan;
            IsActive = true;
        }

        public void Update(string fullName, string email, decimal monthlyValue, PlanType plan)
        {
            if (!IsActive) throw new InvalidOperationException("Não foi possível editar assitante.");

            Validate(fullName, email, SubscriptionDate, monthlyValue);

            FullName = fullName;
            MonthlyValue = monthlyValue;
            Plan = plan;
            Email = email;
        }

        public void Deactivate() => IsActive = false;

        private void Validate(string fullName, string email, DateTime date, decimal value)
        {
            if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("É necessário nome completo.");
            if (date > DateTime.Now) throw new ArgumentException("A data de assinatura não pode ser futura.");
            if (value <= 0) throw new ArgumentException("O valor mensal deve ser maior que zero..");
            if (!email.Contains("@")) throw new ArgumentException("Formato do email inválido.");
        }

        private int CalculateMonths()
        {
            var months = ((DateTime.Now.Year - SubscriptionDate.Year) * 12) + DateTime.Now.Month - SubscriptionDate.Month;
            return months <= 0 ? 1 : months;
        }
    }
}
