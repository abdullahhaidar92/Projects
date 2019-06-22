--drop procedure getAverageOrders 

--returns the average of the revenue made
--in a day, month and year 
Create Procedure getAverageOrders
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
set @daily = (select avg(total) 
		    from (select sum((PRICE-Current_Price)*Quantity) as total
            from Order_Products_ExtraInformation
            where date between @date1 and @date2 
		    group by OID ) subquery)
		   


set @date1=(select GETDATE()-30)
set @monthly = (select avg(total) 
		    from (select sum((PRICE-Current_Price)*Quantity) as total
            from Order_Products_ExtraInformation
            where date between @date1 and @date2 
		    group by OID ) subquery)



set @date1=(select GETDATE()-256)
set @yearly = (select avg(total) 
		    from (select sum((PRICE-Current_Price)*Quantity) as total
            from Order_Products_ExtraInformation
            where date between @date1 and @date2 
		    group by OID ) subquery)
set @currancy=(select top 1 * from (select total_currancy from [Order])subquery );

select @daily as daily ,@monthly as monthly,@yearly as yearly,@currancy as currancy

end

---exec getAverageOrders
