--drop procedure getAllRevenue 
--returns the sum of the revenue made
--in a day, month and year  
Create Procedure getAllRevenue
as
begin
declare @date1 Date;
declare @date2 Date;
declare @daily decimal;
declare @monthly decimal;
declare @yearly decimal;
declare @currancy varchar(10);
set @date2=(select GETDATE())

set @date1=(select GETDATE()-1)
set @daily= (select sum((PRICE-Current_Price)*Quantity) 
            from Order_Products_ExtraInformation
            where date between @date1 and @date2 
		   )

set @date1=(select GETDATE()-30)
set @monthly= (select sum((PRICE-Current_Price)*Quantity) 
           from Order_Products_ExtraInformation
            where date between @date1 and @date2 
			)

set @date1=(select GETDATE()-256)
set @yearly= (select sum((PRICE-Current_Price)*Quantity) 
             from Order_Products_ExtraInformation
             where date between @date1 and @date2 
			)
set @currancy=(select top 1 * from (select total_currancy from [Order])subquery );
select @daily as daily ,@monthly as monthly,@yearly as yearly,@currancy as currancy


end

---exec getAllRevenue
