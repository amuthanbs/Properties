USE [EnManai]
GO
/****** Object:  Table [dbo].[HouseOwner]    Script Date: 8/21/2022 6:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HouseOwner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[FatherName] [nvarchar](max) NULL,
	[MotherName] [nvarchar](max) NULL,
	[AadharNo] [nvarchar](50) NULL,
	[PhonePrimary] [nvarchar](30) NOT NULL,
	[Phone2] [nvarchar](30) NULL,
	[LandLine1] [nvarchar](30) NULL,
	[LandLine2] [nvarchar](30) NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[CIty] [nvarchar](max) NULL,
	[District] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Pincode] [nchar](10) NULL,
	[ResidingAddress] [bit] NOT NULL,
 CONSTRAINT [PK_HouseOwner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HouseOwnerResidingAddress]    Script Date: 8/21/2022 6:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HouseOwnerResidingAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HouseOwnerID] [int] NOT NULL,
	[Address1] [nvarchar](max) NOT NULL,
	[Address2] [nvarchar](max) NOT NULL,
	[CIty] [nvarchar](max) NOT NULL,
	[District] [nvarchar](max) NOT NULL,
	[State] [nvarchar](max) NOT NULL,
	[Pincode] [nchar](10) NULL,
 CONSTRAINT [PK_ouseOwnerResidingAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentalHouseDetails]    Script Date: 8/21/2022 6:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentalHouseDetails](
	[Id] [int] NOT NULL,
	[HouseOwnerId] [int] NOT NULL,
	[FlatNoOrDoorNo] [nvarchar](50) NOT NULL,
	[Address1] [nvarchar](max) NOT NULL,
	[Address2] [nvarchar](max) NOT NULL,
	[AreaOrNagar] [nvarchar](max) NOT NULL,
	[City] [nvarchar](max) NOT NULL,
	[District] [nvarchar](max) NOT NULL,
	[State] [nvarchar](max) NOT NULL,
	[Pincode] [nvarchar](20) NOT NULL,
	[Floor] [nvarchar](10) NOT NULL,
	[Vasuthu] [bit] NOT NULL,
	[Co-OperationWater] [bit] NOT NULL,
	[BoreWater] [bit] NOT NULL,
	[SeparateEB] [bit] NOT NULL,
	[TwoWheelerParking] [bit] NOT NULL,
	[FourWheelerParking] [bit] NOT NULL,
	[SeparateHouse] [bit] NOT NULL,
	[HouseOwnerResidingInSameBuilding] [bit] NOT NULL,
	[RentalOccupied] [nchar](10) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[HouseOwner] ADD  CONSTRAINT [DF_HouseOwner_ResidingAddress]  DEFAULT ((0)) FOR [ResidingAddress]
GO
/****** Object:  StoredProcedure [dbo].[CREATEMODEL]    Script Date: 8/21/2022 6:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CREATEMODEL]  
(  
     @TableName SYSNAME ,  
     @CLASSNAME VARCHAR(500)   
)  
AS  
BEGIN  
    DECLARE @Result VARCHAR(MAX)  
  
    SET @Result = @CLASSNAME + ' '+ @TableName + '  
{'  
  
SELECT @Result = @Result + '  
    public ' + ColumnType + NullableSign + ' ' + ColumnName + ' { get; set; }'  
FROM  
(  
    SELECT   
        REPLACE(col.NAME, ' ', '_') ColumnName,  
        column_id ColumnId,  
        CASE typ.NAME   
            WHEN 'bigint' THEN 'long'  
            WHEN 'binary' THEN 'byte[]'  
            WHEN 'bit' THEN 'bool'  
            WHEN 'char' THEN 'string'  
            WHEN 'date' THEN 'DateTime'  
            WHEN 'datetime' THEN 'DateTime'  
            WHEN 'datetime2' then 'DateTime'  
            WHEN 'datetimeoffset' THEN 'DateTimeOffset'  
            WHEN 'decimal' THEN 'decimal'  
            WHEN 'float' THEN 'float'  
            WHEN 'image' THEN 'byte[]'  
            WHEN 'int' THEN 'int'  
            WHEN 'money' THEN 'decimal'  
            WHEN 'nchar' THEN 'char'  
            WHEN 'ntext' THEN 'string'  
            WHEN 'numeric' THEN 'decimal'  
            WHEN 'nvarchar' THEN 'string'  
            WHEN 'real' THEN 'double'  
            WHEN 'smalldatetime' THEN 'DateTime'  
            WHEN 'smallint' THEN 'short'  
            WHEN 'smallmoney' THEN 'decimal'  
            WHEN 'text' THEN 'string'  
            WHEN 'time' THEN 'TimeSpan'  
            WHEN 'timestamp' THEN 'DateTime'  
            WHEN 'tinyint' THEN 'byte'  
            WHEN 'uniqueidentifier' THEN 'Guid'  
            WHEN 'varbinary' THEN 'byte[]'  
            WHEN 'varchar' THEN 'string'  
            ELSE 'UNKNOWN_' + typ.NAME  
        END ColumnType,  
        CASE   
            WHEN col.is_nullable = 1 and typ.NAME in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier')   
            THEN '?'   
            ELSE ''   
        END NullableSign  
    FROM SYS.COLUMNS col join sys.types typ on col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id  
    where object_id = object_id(@TableName)  
) t  
ORDER BY ColumnId  
SET @Result = @Result  + '  
}'  
  
print @Result  
  
END  
GO
/****** Object:  StoredProcedure [dbo].[EM_HouseOwner]    Script Date: 8/21/2022 6:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Amuthan
-- Create date: 21.08.2022
-- Description:	HouseOwner procedure for Read, Create, Update and Delete (0,1,2,3) are completed.

--@ACTION : 
--0 - READ
--1 - CREATE
--2 - Update
--4 - DELETE
--@ResidingAddress 
--0 - House Owner is Same house
--1 - House Owner is Different house

--EXECUTE EM_HouseOwner 
--	@id='11',
--	@name = 'Ahamed',
--    @LastName = 'Mohamed',
--    @FatherName = 'Syed',
--    @MotherName = 'Bhuavam',
--    @AadharNo  = '1502',
--    @PhonePrimary = '9952401985',
--    @Phone2 = null,
--    @LandLine1  = '0458',
--    @LandLine2 = null,
--    @EmailAddress = 'ahamed@gmail.com',
--    @Address1 = '41',
--    @Address2 = 'Gandhi Road',
--    @CIty = 'Palani',
--    @District = 'Dindigul',
--    @State = 'TN',
--    @Pincode = '624601',
--    @ResidingAddress = 0,
--	@OwnerAddress1  = 'No.102',
--    @OwnerAddress2 = 'Samuvel Puram',
--    @ownerCIty  = 'Thirunelveli',
--    @OwnerDistrict  = 'Thirunelveli',
--    @OwnerState = 'Tamilnadu',
--    @OwnerPincode  = '628002',
--	@Action = '2'

-- =============================================
CREATE PROCEDURE [dbo].[EM_HouseOwner] 
	-- Add the parameters for the stored procedure here
	@id int = 0,
	@name varchar(max),
    @LastName varchar(max) = 'Balan',
    @FatherName varchar(max) = 'Bala Subramanian',
    @MotherName varchar(max) = 'Shanthi1',
    @AadharNo varchar(max) = '1602',
    @PhonePrimary varchar(max) = '9952401983',
    @Phone2 varchar(max) = null,
    @LandLine1 varchar(max) = '04547',
    @LandLine2 varchar(max) = null,
    @EmailAddress varchar(max) = 'amuphy1@gmail.com',
    @Address1 varchar(max) = '43',
    @Address2 varchar(max) = 'Gandhi Road1',
    @CIty varchar(max) = 'Palani1',
    @District varchar(max) = 'Dindigul1',
    @State varchar(max) = 'Tamilnadu1',
    @Pincode varchar(max) = '624602',
    @ResidingAddress bit = 1,
	@OwnerAddress1 nvarchar(max) = null,
    @OwnerAddress2 nvarchar(50) = null,
    @ownerCIty nvarchar(50) = null,
    @OwnerDistrict nvarchar(50) = null,
    @OwnerState nvarchar(50) = null,
    @OwnerPincode nvarchar(50) = null,
	@Action nvarchar(50) = '0'
      

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	If(@Action = '0') --Read 
	BEGIN
	SELECT * FROM HouseOwner H Left join HouseOwnerResidingAddress rd 
	on h.Id = rd.HouseOwnerID
	END
	ELSE If(@Action = '1') --  CREATE
	BEGIN
	DECLARE @newID int
	Insert into houseowner (
                                [Name],
                                LastName,
                                FatherName, 
                                MotherName,
                                AadharNo, 
                                PhonePrimary, 
                                Phone2, 
                                LandLine1,
                                LandLine2, 
                                EmailAddress, 
                                Address1, 
                                Address2, 
                                CIty, 
                                District, 
                                State, 
                                Pincode, 
                                ResidingAddress) 
                                values(@name,
                                @LastName,
                                @FatherName,
                                @MotherName,
                                @AadharNo,
                                @PhonePrimary,
                                @Phone2,
                                @LandLine1,
                                @LandLine2,
                                @EmailAddress,
                                @Address1,
                                @Address2,
                                @CIty,
                                @District,
                                @State,
                                @Pincode,
                                @ResidingAddress)
	declare @IdentityValue int
	 set @IdentityValue = SCOPE_IDENTITY()
	if(@ResidingAddress = 0)
	BEGIN
	PRINT 'Inserting Residing Address'
	INSERT INTO HouseOwnerResidingAddress (
	HouseOwnerID,
	Address1,
	Address2,
	CIty,
	District,
	[State],
	Pincode) 
	values(
	@IdentityValue,
	@OwnerAddress1,
	@OwnerAddress2,
	@ownerCIty,
	@OwnerDistrict,
	@OwnerState,
	@OwnerPincode)
	END
	END
	ELSE IF(@Action ='2') -- Update
	BEGIN 
	PRINT 'Updating Houseowner'
	UPDATE houseowner SET 
		[Name] = @name,
        LastName = @LastName,
        FatherName = @FatherName, 
        MotherName = @MotherName,
        AadharNo = @AadharNo, 
        PhonePrimary = @PhonePrimary, 
        Phone2 = @Phone2, 
        LandLine1 = @LandLine1,
        LandLine2 = @LandLine2, 
        EmailAddress = @EmailAddress, 
        Address1 = @Address1, 
        Address2 = @Address2, 
        CIty = @CIty, 
        District = @District, 
        [State] = @State, 
        Pincode = @Pincode, 
        ResidingAddress = @ResidingAddress 
		WHERE Id=@id
		PRINT 'Updating Houseowner Completed'
		if(@ResidingAddress=0)
		BEGIN
		PRINT 'Updating House owner Residing Address'
		IF EXISTS(select * from HouseOwnerResidingAddress where HouseOwnerID=@id)
		BEGIN
			update HouseOwnerResidingAddress set 
			Address1 = @OwnerAddress1,
			Address2 = @OwnerAddress2,
			CIty = @ownerCIty,
			District = @OwnerDistrict,
			[State] = @OwnerState,
			Pincode	=@OwnerPincode
			where HouseOwnerID=@id
			PRINT 'Updating House owner Residing Address Completed'
		END
		ELSE
		BEGIN
			INSERT INTO HouseOwnerResidingAddress (
			HouseOwnerID,
			Address1,
			Address2,
			CIty,
			District,
			[State],
			Pincode) 
			values(
			@id,
			@OwnerAddress1,
			@OwnerAddress2,
			@ownerCIty,
			@OwnerDistrict,
			@OwnerState,
			@OwnerPincode)
		END
   END
   ELSE IF (@ResidingAddress = 1)
   BEGIN
   IF EXISTS(SELECT * FROM HouseOwnerResidingAddress WHERE HouseOwnerID = @id)
   BEGIN
   DELETE FROM HouseOwnerResidingAddress WHERE HouseOwnerID = @id
   END

   END
	END
	ELSE IF (@Action = '3') --Delete
	BEGIN
	DELETE FROM HouseOwnerResidingAddress WHERE Id=@id
	DELETE FROM HouseOwner WHERE ID=@id
	END
END
GO
