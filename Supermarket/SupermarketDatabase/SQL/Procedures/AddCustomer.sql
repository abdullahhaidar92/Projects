
--drop Procedure AddCustomer
Create Procedure AddCustomer @CID char(5),
                           @FNAME char(20),                    
						   @LNAME char(20), 
						   @BIRTHDATE date,
						   @ADDRESS char(20),
						   @PHONE char(10),
						   @GENDER char(10)
As
Begin
Insert into CUSTOMER values(@CID,@FNAME,@LNAME,@BIRTHDATE,@ADDRESS,@PHONE,@GENDER);
End
