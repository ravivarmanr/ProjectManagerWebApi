using System;
using NBench;
using System.Collections.Generic;
using ProjectManager.BusinessLayer;
using ProjectManager.DataLayer;
using ProjectManager.Entity;


namespace ProjectManager.PerformanceTest
{

    public class ProjectManagerPerformanceTest
    {
        private IProjectManagerBL _projectManagerService;
        private TaskEntity _newTask;
        private TaskEntity _taskToBeUpdated;


        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            IProjectManagerDL _dbRepo = new ProjectManagerDL();
            _projectManagerService = new ProjectManagerBL(_dbRepo);
            _newTask = new TaskEntity { TaskId = 1, Task = "Test project Creation", ParentId = 1, ParentTask = "Test project Creation", TaskPriority = 1, TaskStatus = "Y", StartDate = new DateTime(2018, 9, 9), EndDate = new DateTime(2018, 9, 9), ProjectId = 1, UserId = 2, AddDate = new DateTime(2018, 9, 9), UpdtDate = new DateTime(2018, 9, 9) };
            _taskToBeUpdated = new TaskEntity { TaskId = 2, Task = "Setup Creation", ParentId = 1, ParentTask = "Test project Creation", TaskPriority = 2, TaskStatus = "Y", StartDate = new DateTime(2018, 9, 9), EndDate = new DateTime(2018, 9, 9), ProjectId = 2, UserId = 1, AddDate = new DateTime(2018, 9, 9), UpdtDate = new DateTime(2018, 9, 9) };

        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 3000)]
        public void GetAll_TaskList_Elapsed_Time()
        {
            var result = _projectManagerService.GetAllTasks();
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        public void GetAll_TaskList_Memory_Consumed()
        {
            var result = _projectManagerService.GetAllTasks();
        }


        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 3000)]
        public void Add_Task_Elapsed_Time()
        {
            _projectManagerService.AddTask(_newTask);
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        public void Add_Task_Memory_Consumed()
        {
            _projectManagerService.AddTask(_newTask);
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 3000)]
        public void Update_Task_Elapsed_Time()
        {
            _projectManagerService.UpdateTask(_taskToBeUpdated);
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        public void Update_Task_Memory_Consumed()
        {
            _projectManagerService.UpdateTask(_taskToBeUpdated);
        }

    }
}
