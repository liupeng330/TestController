use TestJobDB

select * from dbo.TaskGroup
select TaskGroupID, TaskID, TaskOrder from dbo.Task_TaskGroup order by TaskGroupID
select * from dbo.Task

select * from dbo.JobGroup
select * from dbo.Job
select * from dbo.Client_JobGroup