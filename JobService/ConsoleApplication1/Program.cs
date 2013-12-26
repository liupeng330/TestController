using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TestTechnology.DAL.Models;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TestJobDBContext())
            {
                JobGroup jobGroup = new JobGroup()
                {
                    JobGroupName = "TestGroup1",
                    JobGroupResult = 1,
                };
                db.JobGroups.Add(jobGroup);

                Job job = new Job()
                {
                    JobGroup = jobGroup,
                    JobName = "TestJob1",
                    StartTime = DateTime.Now,
                    ExecutePath = @"c:\abc\def\aaa.exe",
                    ExecuteArgs = @"aaa aaa -c",
                    JobOrder = 1,
                    JobResult = 1,
                };
                db.Jobs.Add(job);

                JobAssignment assignment = new JobAssignment()
                {
                    ClientID = "abc",
                    JobGroup = jobGroup,
                    JobAssignmentDateTime = DateTime.Now,
                };
                db.JobAssignments.Add(assignment);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
        }
    }

    class Person
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }

    class PersonDTO
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
