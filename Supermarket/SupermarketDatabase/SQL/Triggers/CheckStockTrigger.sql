--drop trigger CheckStockTrigger
Create trigger CheckStockTrigger on ORDER_ITEMS for insert
as
begin
declare @pid char(5),
	   @quantity int,
	   @stock_quantity int
set @quantity= (select inserted.QUANTITY from inserted)
set @pid= (select inserted.PID from inserted)
set @stock_quantity=(select QUANTITY from Stock where PID=@pid)
if (@stock_quantity < @quantity)
begin
print 'Not Enough Quantity In The Stock'
rollback
return
end
update Stock
set QUANTITY=QUANTITY-@quantity

end