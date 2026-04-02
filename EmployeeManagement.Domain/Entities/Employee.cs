// EmployeeManagement.Domain/Entities/Employee.cs
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Domain.Entities
{

    public class Employee
    {
        public int Id { get; private set; }

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public decimal Salary { get; private set; }
        public bool IsActive { get; private set; } = true;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        // Parameterless constructor for EF Core
        private Employee() { }

        // Factory method with business rules
        public static Employee Create(string firstName, string lastName, string email,
                                      DateTime dateOfBirth, decimal salary)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");
            if (dateOfBirth == default || dateOfBirth > DateTime.UtcNow)
                throw new ArgumentException("Invalid date of birth.");
            if (CalculateAge(dateOfBirth) < 18)
                throw new ArgumentException("Employee must be at least 18 years old.");
            if (salary <= 0)
                throw new ArgumentException("Salary must be greater than zero.");

            return new Employee
            {
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                Email = email.Trim().ToLower(),
                DateOfBirth = dateOfBirth.Date,
                Salary = salary,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }

        private static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.UtcNow.Date;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age;
        }

        // Business methods
        public void UpdateDetails(string firstName, string lastName, decimal salary)
        {
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Salary = salary;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}