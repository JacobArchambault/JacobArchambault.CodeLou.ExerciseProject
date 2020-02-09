using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using static JacobArchambault.CodeLou.ExerciseProject.Menu;
using static System.Console;
using static System.IO.File;
using static System.Text.Json.JsonSerializer;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    static class Program
    {

        static async Task Main()
        {
            string jsonFile = @"..\..\..\student.json";
            int response;
            List<Student> listOfStudents;

            try
            {
                using FileStream fs = OpenRead(jsonFile);
                listOfStudents = await DeserializeAsync<List<Student>>(fs);
            }
            catch (FileNotFoundException)
            {
                listOfStudents = new List<Student> { };
            }
            catch (JsonException)
            {
                listOfStudents = new List<Student> { };
            }

            WriteLine("Welcome to your student center.");

            do
            {
                PrintMainMenu();
                _ = int.TryParse(ReadLine(), out response);
                Choose(response, listOfStudents);
            } while (response == 1 || response == 2 || response == 3);

            using (FileStream fs = Create(jsonFile))
            {
                await SerializeAsync(fs, listOfStudents, new JsonSerializerOptions { WriteIndented = true });
            }
        }
    }
}