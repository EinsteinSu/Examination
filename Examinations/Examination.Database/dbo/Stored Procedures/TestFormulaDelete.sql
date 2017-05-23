CREATE procedure TestFormulaDelete
@formulaId int
as

begin try
begin transaction

declare @testPaperId int
select @testPaperId = TestPaperId from TestPaperFormulas

delete from TestPaperQuestions 
where 
exists(
	select top 1 1 from TestPaperFormulas as tf
	where tf.TestPaperId = TestPaperQuestions.TestPaperId and tf.FormulaId = @formulaId
	)


delete from TestPaperFormulas where FormulaId = @formulaId

commit transaction
end try
begin catch
Raiserror('Delete test formula error',16,1)
rollback transaction
end catch