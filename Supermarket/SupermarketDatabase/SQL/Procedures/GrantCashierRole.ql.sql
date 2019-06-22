--drop Procedure GrantCashierRole
Create Procedure GrantCashierRole @EID char(5)
AS
Begin
declare @sql char(200)
set @sql='Alter Role Cashier Add Member '+@EID;
exec(@sql)
Update Employee
set Position='Cashier'
where EID=@EID
End 
--select * from Employee