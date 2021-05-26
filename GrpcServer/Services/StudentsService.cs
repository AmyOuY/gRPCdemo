using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class StudentsService : Student.StudentBase
    {
        private readonly ILogger<StudentsService> _logger;

        public StudentsService(ILogger<StudentsService> logger)
        {
            _logger = logger;
        }


        public override Task<StudentModel> GetStudentInfo(StudentLookupModel request, ServerCallContext context)
        {
            StudentModel output = new StudentModel();
            if (request.StudentId == 1)
            {
                output.FirstName = "Amy";
                output.LastName = "Ou";
                output.Email = "amy@amy.com";
            }
            else if (request.StudentId == 2)
            {
                output.FirstName = "Leo";
                output.LastName = "King";
                output.Email = "leo@king.net";
            }
            else
            {
                output.FirstName = "Sunny";
                output.LastName = "Lee";
                output.Email = "sunny@lee.com";
            }

            return Task.FromResult(output);
        }


        public override async Task GetNewStudents(NewStudentsRequest request, IServerStreamWriter<StudentModel> responseStream, ServerCallContext context)
        {
            List<StudentModel> students = new List<StudentModel> {
                new StudentModel
                {
                    FirstName = "Larry",
                    LastName = "Kong",
                    Email = "larry@kong.com",
                    IsRegistered = true,
                    Age = 22

                },
                new StudentModel
                {
                    FirstName = "Lian",
                    LastName = "May",
                    Email = "lian@may.net",
                    IsRegistered = false,
                    Age = 20
                },
                new StudentModel
                {
                    FirstName = "Scott",
                    LastName = "Smith",
                    Email = "scott@smith.com",
                    IsRegistered = true,
                    Age = 19
                }
            };

            foreach (var student in students)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(student);
            }

        }
    }
}
