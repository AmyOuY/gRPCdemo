using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest { Name = "Amy" };
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(input);
            //Console.WriteLine(reply.Message);


            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var studentClient = new Student.StudentClient(channel);
            var studentRequested = new StudentLookupModel { StudentId = 2 };
            var student = await studentClient.GetStudentInfoAsync(studentRequested);
            Console.WriteLine($"{ student.FirstName } { student.LastName }: { student.Email }");

            Console.WriteLine();

            using (var call = studentClient.GetNewStudents(new NewStudentsRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentStudent = call.ResponseStream.Current;
                    Console.WriteLine($"{ currentStudent.FirstName } { currentStudent.LastName}: { currentStudent.Age }, {currentStudent.IsRegistered }");
                }
            }

            Console.ReadLine();
        }
    }
}
