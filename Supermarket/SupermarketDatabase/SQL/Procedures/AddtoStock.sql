--drop procedure AddtoStock
Create  procedure AddtoStock @SID char(5),
						   @QUANTITY int
as
Begin 
declare @MAX int,
	   @stock_quantity int
set @stock_quantity=(select QUANTITY from Stock where SID=@SID)
set @MAX=(select MAX from Stock where SID=@SID)
if ((@stock_quantity+@QUANTITY) > @MAX)
begin
print 'MAXIMUM is REACHED'
return
end

Update STOCK 
set QUANTITY=@stock_quantity+@QUANTITY
where SID=@SID

end


select * from STOCK