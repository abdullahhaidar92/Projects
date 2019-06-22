--use SuperMarket
--drop Procedure AddEmployee
Create Procedure AddEmployee @EID char(5),
						   @FNAME char(20),                    
						   @LNAME char(20), 
						   @BIRTHDATE date,
						   @ADDRESS char(20),
						   @PHONE char(10),
						   @GENDER char(10),
						   @SALARY float,
						   @SALARY_CURRANCY char(5),
						   @TYPE char(20)
As
Begin
Insert into EMPLOYEE values(@EID,@FNAME,@LNAME,@BIRTHDATE,@ADDRESS,@PHONE,@GENDER,@SALARY,@SALARY_CURRANCY,@TYPE);
End
