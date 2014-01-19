using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper;
using TestTechnology.Controller.BIZ;
using TestTechnology.DAL;
using TestTechnology.DAL.Models;
using TestTechnology.Shared.DTO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TestJobDBContext())
            {
                ////Task
                //TestTechnology.DAL.Models.Task task = new TestTechnology.DAL.Models.Task()
                //{
                //    TaskName = "Test Task",
                //    TaskExecuteFilePath = "abcde",
                //};
                //db.Tasks.Add(task);

                //TaskGroup taskGroup = new TaskGroup()
                //{
                //    TaskGroupName = "Task Group Name",
                //};
                //db.TaskGroups.Add(taskGroup);

                //Task_TaskGroup task_taskgroup = new Task_TaskGroup()
                //{
                //    Task = task,
                //    TaskGroup = taskGroup,
                //    TaskOrder = 1,
                //};
                //db.Task_TaskGroup.Add(task_taskgroup);


                //Job
                //var taskGroup = (from i in db.TaskGroups where i.TaskGroupName == "Task Group Name" select i).Single();
                //JobGroup jobGroup = new JobGroup()
                //{
                //    TaskGroup = taskGroup,
                //    StartTime = DateTime.Now,
                //    Status = 1,
                //};
                //db.JobGroups.Add(jobGroup);

                //var task = (from i in db.Tasks where i.TaskName == "Test Task" select i).Single();
                //Job job = new Job()
                //{
                //    Task = task,
                //    StartTime = DateTime.Now,
                //    JobGroup = jobGroup,
                //    Status = 2,
                //};
                //db.Jobs.Add(job);

                //JobAssignment
                //JobAssigment assigment = new JobAssigment()
                //{
                //    StartTime = DateTime.Now,
                //    Owner = "PengLiu",
                //    Status = 2,
                //    Result = 1,
                //};
                //db.JobAssigments.Add(assigment);

                //var jobGroup = (from i in db.JobGroups where i.JobGroupID == 1 select i).Single();
                //Client_Job_Assignment clientJobAssignment = new Client_Job_Assignment()
                //{
                //    ClientID = Guid.NewGuid().ToString(),
                //    JobAssigment = assigment,
                //    JobGroup = jobGroup,
                //};
                //db.Client_Job_Assignment.Add(clientJobAssignment);

                //try
                //{
                //    db.SaveChanges();
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e);
                //    throw;
                //}

                //Mapper.CreateMap<Person, PersonDTO>();
                //Mapper.CreateMap<BigPerson, PersonDTO>();
                //Person p = new Person()
                //{
                //    Id = "123",
                //    Name = "PengLiu",
                //};

                //BigPerson bp = new BigPerson()
                //{
                //    Status = 1,
                //    Age = 40,
                //};

                //PersonDTO pDTO = EntityMapper.Map<PersonDTO>(p, bp);

                //Mapper.CreateMap<PersonDTO, Person>();
                //PersonDTO pDTO = new PersonDTO()
                //{
                //    Name = "personDTO",
                //    Id = "123",
                //    Status = 2,
                //    Age = 22,
                //};

                //Person p = Mapper.Map<Person>(pDTO);

                //Mapper.CreateMap<PersonDTO, Person>();
                //PersonDTO pDTO = new PersonDTO()
                //{
                //    Name = "LiuPeng",
                //    Id = "123",
                //    //StartTime = DateTime.Now,
                //};

                //var p = Mapper.Map<Person>(pDTO);

            }
        }
    }

    class Person
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public Nullable<DateTime> StartTime { get; set; }
    }

    class BigPerson
    {
        public int Status { get; set; }
        public int Age { get; set; }
    }

    class PersonDTO
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int Status { get; set; }
        public int Age { get; set; }
        public DateTime StartTime { get; set; }
    }

    public static class EntityMapper
    {
        public static T Map<T>(params object[] sources) where T : class
        {
            if (!sources.Any())
            {
                return default(T);
            }

            var initialSource = sources[0];

            var mappingResult = Map<T>(initialSource);

            // Now map the remaining source objects
            if (sources.Count() > 1)
            {
                Map(mappingResult, sources.Skip(1).ToArray());
            }

            return mappingResult;
        }

        private static void Map(object destination, params object[] sources)
        {
            if (!sources.Any())
            {
                return;
            }

            var destinationType = destination.GetType();

            foreach (var source in sources)
            {
                var sourceType = source.GetType();
                Mapper.Map(source, destination, sourceType, destinationType);
            }
        }

        private static T Map<T>(object source) where T : class
        {
            var destinationType = typeof(T);
            var sourceType = source.GetType();

            var mappingResult = Mapper.Map(source, sourceType, destinationType);

            return mappingResult as T;
        }
    }

}
