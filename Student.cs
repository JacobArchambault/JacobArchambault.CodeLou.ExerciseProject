using System;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    internal class Student
    {
        internal int StudentId { get; set; }
        internal string FirstName { get; set; }
        internal string LastName { get; set; }
        internal string Name => $"{FirstName} {LastName}";
        internal string ClassName { get; set; }
        internal DateTimeOffset StartDate { get; set; }
        internal string LastClassCompleted { get; set; }
        internal DateTimeOffset LastClassCompletedOn { get; set; }
    }
}
