
CREATE TABLE [dbo].[BCSScheduleFrequency](
	[BCSScheduleFrequencyID] [int] IDENTITY(1,1) NOT NULL,
	[FrequencyType] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_BCSScheduleFrequency] PRIMARY KEY CLUSTERED 
(
	[BCSScheduleFrequencyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[BCSFrequencyDayOfTheWeek](
	[BCSFrequencyDayOfTheWeekID] [int] IDENTITY(1,1) NOT NULL,
	[DayOfTheWeekType] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_BCSFrequencyDayOfTheWeek] PRIMARY KEY CLUSTERED 
(
	[BCSFrequencyDayOfTheWeekID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[BCSWeekForTheMonth](
	[BCSWeekForTheMonthID] [int] IDENTITY(1,1) NOT NULL,
	[WeekForTheMonth] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_BCSWeekForTheMonth] PRIMARY KEY CLUSTERED 
(
	[BCSWeekForTheMonthID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



ALTER TABLE BCSClientIPFileConfig
ADD BCSScheduleFrequencyID int not null default(1),
  BCSFrequencyDayOfTheWeekID int null,
  BCSWeekForTheMonthID int null,
  DailyDayOfWeekFrom int null,
  DailyDayOfWeekTo int null, 
  LastBusinessDayToCountFrom int null

INSERT INTO BCSScheduleFrequency(FrequencyType) VALUES('Daily')
GO
INSERT INTO BCSScheduleFrequency(FrequencyType) VALUES('Weekly')
GO
INSERT INTO BCSScheduleFrequency(FrequencyType) VALUES('Monthly')
GO
INSERT INTO BCSScheduleFrequency(FrequencyType) VALUES('Quarterly')
GO
INSERT INTO BCSScheduleFrequency(FrequencyType) VALUES('Bi-Annually')
GO
INSERT INTO BCSScheduleFrequency(FrequencyType) VALUES('LastBusinessDayOfMonthToSendFrom')
GO

INSERT INTO BCSFrequencyDayOfTheWeek(DayOfTheWeekType)
VALUES('SUNDAY')
GO
INSERT INTO BCSFrequencyDayOfTheWeek(DayOfTheWeekType)
VALUES('MONDAY')
GO
INSERT INTO BCSFrequencyDayOfTheWeek(DayOfTheWeekType)
VALUES('TUESDAY')
GO
INSERT INTO BCSFrequencyDayOfTheWeek(DayOfTheWeekType)
VALUES('WEDNESDAY')
GO
INSERT INTO BCSFrequencyDayOfTheWeek(DayOfTheWeekType)
VALUES('THURSDAY')
GO
INSERT INTO BCSFrequencyDayOfTheWeek(DayOfTheWeekType)
VALUES('FRIDAY')
GO
INSERT INTO BCSFrequencyDayOfTheWeek(DayOfTheWeekType)
VALUES('SATURDAY')
GO

INSERT INTO BCSWeekForTheMonth(WeekForTheMonth) VALUES('FIRST')
GO
INSERT INTO BCSWeekForTheMonth(WeekForTheMonth) VALUES('SECOND')
GO
INSERT INTO BCSWeekForTheMonth(WeekForTheMonth) VALUES('THIRD')
GO
INSERT INTO BCSWeekForTheMonth(WeekForTheMonth) VALUES('FOURTH')
GO
INSERT INTO BCSWeekForTheMonth(WeekForTheMonth) VALUES('LAST')
GO

update BCSClientIPFileConfig
 set BCSScheduleFrequencyID=1,DailyDayOfWeekFrom=3,DailyDayOfWeekTo=7
 where ClientPrefix ='AEG'
 go

 update BCSClientIPFileConfig
 set BCSScheduleFrequencyID=6,LastBusinessDayToCountFrom=11
 where ClientPrefix ='AB'
 go