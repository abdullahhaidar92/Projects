--drop procedure AddStock
Create  procedure AddStock @SID char(5),
						 @PID char(5),
						 @QUANTITY int,
						 @MIN int,
						 @MAX int
as
Begin
if(@QUANTITY >= @MAX)
	begin
		print 'Maximum is reached'
		rollback
	end
Insert into STOCK 
values (@SID,@PID,@QUANTITY,@MIN,@MAX)
end