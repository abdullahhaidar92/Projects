--drop Procedure GrantStockManagerRole
Create Procedure GrantStockManagerRole @EID char(5)
AS
Begin
declare @sql char(200)
set @sql='Alter Role StockManager Add Member '+@EID;
exec(@sql)
Update Employee
set Position='StockManager'
where EID=@EID
End 
