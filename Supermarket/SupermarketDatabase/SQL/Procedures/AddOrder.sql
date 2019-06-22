--Use SuperMarket
--drop Procedure AddOrder
Create Procedure AddOrder  @OID   int,
						 @CID   char(5)
As
declare @date datetime,
		@EID  char(5)
Select @date=(Select GETDATE());
Select @EID=(Select CURRENT_USER);
Begin
Insert into [ORDER] 
values(@OID,@CID,@EID,0.0,'LLP',@date);
End

