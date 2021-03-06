USE [ProjectA]
GO
/****** Object:  Table [dbo].[Lookup]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](100) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NULL,
	[Title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created_On] [date] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Advisor]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Advisor](
	[Id] [int] NOT NULL,
	[Designation] [int] NOT NULL,
	[Salary] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupProject]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupProject](
	[ProjectId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[AssignmentDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupProject] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] NOT NULL,
	[RegistrationNo] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectAdvisor]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectAdvisor](
	[AdvisorId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[AdvisorRole] [int] NOT NULL,
	[AssignmentDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProjectAdvisor] PRIMARY KEY CLUSTERED 
(
	[AdvisorId] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupStudent]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupStudent](
	[GroupId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[AssignmentDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupStudent] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Studentproject]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Studentproject]
	AS Select Student.Id AS StudentID ,Student.RegistrationNo AS StudentRegistrationNo,L1.Value AS GroupStatus,[Group].Id AS GroupId,GroupStudent.AssignmentDate AS GroupAssignmentDate,L3.Id AS ProjectId,L3.Title AS ProjectTitle,L2.AssignmentDate AS ProjectAssignmentDate,L4.AdvisorId AS AdvisorID,L4.AdvisorRole AS AdvisorRoleID,L6.Value AS Advisor,L7.Value AS AdvisorRole,L4.AssignmentDate AS AdvisorAssignmentDate FROM (((((((((Student LEFT JOIN GroupStudent ON Student.Id=GroupStudent.StudentId)LEFT JOIN Lookup AS L1 ON GroupStudent.Status=L1.Id)FULL JOIN [Group]  ON [Group].Id=GroupStudent.GroupId)FULL JOIN GroupProject AS L2 ON L2.GroupId=[Group].Id)FULL JOIN Project AS L3 ON L3.Id=L2.ProjectId)FULL JOIN ProjectAdvisor AS L4 ON L4.ProjectId=L3.Id)LEFT JOIN Advisor AS L5 ON L5.Id=L4.AdvisorId)LEFT JOIN Lookup AS L6 ON L4.AdvisorId=L6.Id)LEFT JOIN Lookup AS L7 ON L7.Id=L4.AdvisorRole)
GO
/****** Object:  Table [dbo].[Evaluation]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[TotalMarks] [int] NOT NULL,
	[TotalWeightage] [int] NOT NULL,
 CONSTRAINT [PK_Evaluation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupEvaluation]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupEvaluation](
	[GroupId] [int] NOT NULL,
	[EvaluationId] [int] NOT NULL,
	[ObtainedMarks] [int] NOT NULL,
	[EvaluationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupEvaluation] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[EvaluationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Studentevaluation]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Studentevaluation]
	AS Select Student.Id AS StudentID ,Student.RegistrationNo AS StudentRegistrationNo,L1.Value AS GroupStatus,[Group].Id AS GroupId,GroupStudent.AssignmentDate AS GroupAssignmentDate,L3.Id AS ProjectId,L3.Title AS ProjectTitle,L2.AssignmentDate AS ProjectAssignmentDate,L5.Name AS AssignmentName,L4.EvaluationId AS EvaluationID,L5.TotalMarks AS TotalMarks,L5.TotalWeightage AS TotalWeightage,L4.ObtainedMarks AS ObtainedMarks,L4.EvaluationDate AS EvaluationDate FROM (((((((Student LEFT JOIN GroupStudent ON Student.Id=GroupStudent.StudentId)LEFT JOIN Lookup AS L1 ON GroupStudent.Status=L1.Id)FULL JOIN [Group]  ON [Group].Id=GroupStudent.GroupId)FULL JOIN GroupProject AS L2 ON L2.GroupId=[Group].Id)FULL JOIN Project AS L3 ON L3.Id=L2.ProjectId)FULL JOIN GroupEvaluation AS L4 ON L4.GroupId=[Group].Id)LEFT JOIN Evaluation AS L5 ON  L5.Id=L4.EvaluationId)
GO
/****** Object:  View [dbo].[ProjectReport]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProjectReport]
	AS Select Student.Id AS StudentID ,Student.RegistrationNo AS StudentRegistrationNo,L1.Value AS GroupStatus,[Group].Id AS GroupId,GroupStudent.AssignmentDate AS GroupAssignmentDate,L3.Id AS ProjectId,L3.Title AS ProjectTitle,L2.AssignmentDate AS ProjectAssignmentDate,L4.AdvisorId AS AdvisorID,L4.AdvisorRole AS AdvisorRoleID,L6.Value AS Advisor,L7.Value AS AdvisorRole,L4.AssignmentDate AS AdvisorAsssignmentDate FROM (((((((((Student LEFT JOIN GroupStudent ON Student.Id=GroupStudent.StudentId)LEFT JOIN Lookup AS L1 ON GroupStudent.Status=L1.Id) JOIN [Group]  ON [Group].Id=GroupStudent.GroupId)FULL JOIN GroupProject AS L2 ON L2.GroupId=[Group].Id) JOIN Project AS L3 ON L3.Id=L2.ProjectId) JOIN ProjectAdvisor AS L4 ON L4.ProjectId=L3.Id)LEFT JOIN Advisor AS L5 ON L5.Id=L4.AdvisorId)LEFT JOIN Lookup AS L6 ON L4.AdvisorId=L6.Id)LEFT JOIN Lookup AS L7 ON L7.Id=L4.AdvisorRole)
GO
/****** Object:  View [dbo].[EvaluationReport]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[EvaluationReport]
	AS Select Student.Id AS StudentID ,Student.RegistrationNo AS StudentRegistrationNo,L1.Value AS GroupStatus,[Group].Id AS GroupId,GroupStudent.AssignmentDate AS GroupAssignmentDate,L3.Id AS ProjectId,L3.Title AS ProjectTitle,L2.AssignmentDate AS ProjectAssignmentDate,L5.Name AS AssignmentName,L4.EvaluationId AS EvaluationID,L5.TotalMarks AS TotalMarks,L5.TotalWeightage AS TotalWeightage,L4.ObtainedMarks AS ObtainedMarks,L4.EvaluationDate AS EvaluatisonDate FROM (((((((Student LEFT JOIN GroupStudent ON Student.Id=GroupStudent.StudentId)LEFT JOIN Lookup AS L1 ON GroupStudent.Status=L1.Id)JOIN [Group]  ON [Group].Id=GroupStudent.GroupId) JOIN GroupProject AS L2 ON L2.GroupId=[Group].Id) JOIN Project AS L3 ON L3.Id=L2.ProjectId) JOIN GroupEvaluation AS L4 ON L4.GroupId=[Group].Id)LEFT JOIN Evaluation AS L5 ON  L5.Id=L4.EvaluationId)
GO
/****** Object:  Table [dbo].[Person]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NULL,
	[Contact] [varchar](20) NULL,
	[Email] [varchar](30) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[Gender] [int] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Pro]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE VIEW [dbo].[Pro] AS SELECT Person.FirstName,Person.LastName,Person.Email,Student.RegistrationNo FROM Person INNER JOIN  Student ON  Person.Id=Student.Id;
GO
/****** Object:  View [dbo].[vs]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vs] AS SELECT  Person.FirstName AS FirstName,Student.RegistrationNo FROM  Person INNER JOIN  Student ON Person.Id=Student.Id WHERE Student.RegistrationNo='2016-CS-370'
GO
/****** Object:  View [dbo].[sc]    Script Date: 5/3/2019 7:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[sc] AS SELECT  Person.FirstName AS FirstName,Student.RegistrationNo FROM  Person INNER JOIN  Student ON Person.Id=Student.Id WHERE Student.RegistrationNo='2016-CS-370' WITH CHECK OPTION

GO
INSERT [dbo].[Advisor] ([Id], [Designation], [Salary]) VALUES (6, 6, CAST(23000 AS Decimal(18, 0)))
INSERT [dbo].[Advisor] ([Id], [Designation], [Salary]) VALUES (7, 7, CAST(120000 AS Decimal(18, 0)))
INSERT [dbo].[Advisor] ([Id], [Designation], [Salary]) VALUES (10, 10, CAST(12635 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Evaluation] ON 

INSERT [dbo].[Evaluation] ([Id], [Name], [TotalMarks], [TotalWeightage]) VALUES (10, N'Task1', 100, 50)
INSERT [dbo].[Evaluation] ([Id], [Name], [TotalMarks], [TotalWeightage]) VALUES (11, N'Task2', 100, 30)
INSERT [dbo].[Evaluation] ([Id], [Name], [TotalMarks], [TotalWeightage]) VALUES (12, N'Task3', 100, 20)
INSERT [dbo].[Evaluation] ([Id], [Name], [TotalMarks], [TotalWeightage]) VALUES (13, N'Task6', 100, 100)
INSERT [dbo].[Evaluation] ([Id], [Name], [TotalMarks], [TotalWeightage]) VALUES (14, N'task7', 100, 100)
INSERT [dbo].[Evaluation] ([Id], [Name], [TotalMarks], [TotalWeightage]) VALUES (15, N'FYP Proposal', 10, 10)
SET IDENTITY_INSERT [dbo].[Evaluation] OFF
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([Id], [Created_On]) VALUES (60, CAST(N'2019-03-30' AS Date))
INSERT [dbo].[Group] ([Id], [Created_On]) VALUES (61, CAST(N'2019-03-30' AS Date))
INSERT [dbo].[Group] ([Id], [Created_On]) VALUES (62, CAST(N'2019-03-30' AS Date))
INSERT [dbo].[Group] ([Id], [Created_On]) VALUES (63, CAST(N'2019-03-30' AS Date))
INSERT [dbo].[Group] ([Id], [Created_On]) VALUES (64, CAST(N'2019-04-26' AS Date))
SET IDENTITY_INSERT [dbo].[Group] OFF
INSERT [dbo].[GroupEvaluation] ([GroupId], [EvaluationId], [ObtainedMarks], [EvaluationDate]) VALUES (60, 14, 100, CAST(N'2019-04-26T00:00:00.000' AS DateTime))
INSERT [dbo].[GroupEvaluation] ([GroupId], [EvaluationId], [ObtainedMarks], [EvaluationDate]) VALUES (60, 15, 10, CAST(N'2019-04-26T00:00:00.000' AS DateTime))
INSERT [dbo].[GroupEvaluation] ([GroupId], [EvaluationId], [ObtainedMarks], [EvaluationDate]) VALUES (61, 12, 10, CAST(N'2019-04-26T00:00:00.000' AS DateTime))
INSERT [dbo].[GroupProject] ([ProjectId], [GroupId], [AssignmentDate]) VALUES (57, 63, CAST(N'2019-03-30T00:00:00.000' AS DateTime))
INSERT [dbo].[GroupProject] ([ProjectId], [GroupId], [AssignmentDate]) VALUES (58, 61, CAST(N'2019-03-30T00:00:00.000' AS DateTime))
INSERT [dbo].[GroupProject] ([ProjectId], [GroupId], [AssignmentDate]) VALUES (59, 60, CAST(N'2019-04-26T00:00:00.000' AS DateTime))
INSERT [dbo].[GroupStudent] ([GroupId], [StudentId], [Status], [AssignmentDate]) VALUES (60, 1284, 3, CAST(N'2019-04-26T00:00:00.000' AS DateTime))
INSERT [dbo].[GroupStudent] ([GroupId], [StudentId], [Status], [AssignmentDate]) VALUES (60, 1285, 4, CAST(N'2019-04-26T00:00:00.000' AS DateTime))
INSERT [dbo].[GroupStudent] ([GroupId], [StudentId], [Status], [AssignmentDate]) VALUES (60, 1286, 3, CAST(N'2019-04-26T00:00:00.000' AS DateTime))
INSERT [dbo].[GroupStudent] ([GroupId], [StudentId], [Status], [AssignmentDate]) VALUES (61, 1282, 3, CAST(N'2019-03-30T00:00:00.000' AS DateTime))
INSERT [dbo].[GroupStudent] ([GroupId], [StudentId], [Status], [AssignmentDate]) VALUES (61, 1283, 3, CAST(N'2019-03-30T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Lookup] ON 

INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (1, N'Male', N'GENDER')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (2, N'Female', N'GENDER')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (3, N'Active', N'STATUS')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (4, N'InActive', N'STATUS')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (6, N'Professor', N'DESIGNATION')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (7, N'Associate Professor', N'DESIGNATION')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (8, N'Assisstant Professor', N'DESIGNATION')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (9, N'Lecturer', N'DESIGNATION')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (10, N'Industry Professional', N'DESIGNATION')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (11, N'Main Advisor', N'ADVISOR_ROLE')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (12, N'Co-Advisror', N'ADVISOR_ROLE')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (14, N'Industry Advisor', N'ADVISOR_ROLE')
SET IDENTITY_INSERT [dbo].[Lookup] OFF
SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1278, N'Fatima', N'Khalil', N'+92-3216117957', N'fatima@gmail.com', CAST(N'2019-03-30T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1279, N'Ayesha', N'Butt', N'+92-3216117957', N'ayesha@gmail.com', CAST(N'2019-03-30T15:27:27.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1280, N'Ozz', N'Khan', N'+92-3216117957', N'ozz@gmail.com', CAST(N'2019-03-30T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1281, N'Amina', N'Butt', N'+92-3216117967', N'amina@gmail.com', CAST(N'2019-03-30T17:20:23.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1282, N'Fat', N'Butt', N'+92-3217117957', N'Fat@gmail.com', CAST(N'2019-03-30T17:21:07.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1283, N'Ertugral', N'Khan', N'+92-3216117957', N'fat@gmail.com', CAST(N'2019-03-30T17:22:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1284, N'fad', N'gutt', N'+92-3216117957', N'gg@gmail.com', CAST(N'2019-03-30T17:22:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1285, N'Aleena', N'dar', N'+92-3216117957', N'aleena@gmail.com', CAST(N'2019-03-30T18:45:31.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1286, N'Mariam', N'Saeed', N'+92-3216117957', N'mariam@gmail.com', CAST(N'2019-03-30T18:49:38.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1287, N'Fatima', N'Butt', N'+92-3216117957', N'fatima@gmail.com', CAST(N'2019-03-30T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1288, N'fatima', N'Butt', N'+92-1234567890', N'fatima@gmail.com', CAST(N'2019-04-26T00:00:00.000' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Person] OFF
SET IDENTITY_INSERT [dbo].[Project] ON 

INSERT [dbo].[Project] ([Id], [Description], [Title]) VALUES (55, N'Programming', N'PF')
INSERT [dbo].[Project] ([Id], [Description], [Title]) VALUES (57, N'ProjectManagemnet', N'PM')
INSERT [dbo].[Project] ([Id], [Description], [Title]) VALUES (58, N'Computing', N'ITC')
INSERT [dbo].[Project] ([Id], [Description], [Title]) VALUES (59, N'mnbncx', N'Cs')
SET IDENTITY_INSERT [dbo].[Project] OFF
INSERT [dbo].[ProjectAdvisor] ([AdvisorId], [ProjectId], [AdvisorRole], [AssignmentDate]) VALUES (6, 58, 12, CAST(N'2019-03-30T00:00:00.000' AS DateTime))
INSERT [dbo].[ProjectAdvisor] ([AdvisorId], [ProjectId], [AdvisorRole], [AssignmentDate]) VALUES (7, 55, 14, CAST(N'2019-03-30T00:00:00.000' AS DateTime))
INSERT [dbo].[ProjectAdvisor] ([AdvisorId], [ProjectId], [AdvisorRole], [AssignmentDate]) VALUES (7, 57, 14, CAST(N'2019-03-30T00:00:00.000' AS DateTime))
INSERT [dbo].[ProjectAdvisor] ([AdvisorId], [ProjectId], [AdvisorRole], [AssignmentDate]) VALUES (10, 57, 11, CAST(N'2019-04-26T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1278, N'2016-CRP-370')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1279, N'2016-CS-377')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1280, N'2016-CS-366')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1281, N'2016-CS-379')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1282, N'2016-CS-367')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1283, N'2016-CS-365')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1284, N'2016-CRP-432')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1285, N'2016-CS-378')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1286, N'2016-CRP-378')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1287, N'2016-EE-370')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1288, N'2016-CS-370')
ALTER TABLE [dbo].[Advisor]  WITH CHECK ADD  CONSTRAINT [FK_Advisor_Lookup] FOREIGN KEY([Designation])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Advisor] CHECK CONSTRAINT [FK_Advisor_Lookup]
GO
ALTER TABLE [dbo].[GroupEvaluation]  WITH CHECK ADD  CONSTRAINT [FK_GroupEvaluation_Evaluation] FOREIGN KEY([EvaluationId])
REFERENCES [dbo].[Evaluation] ([Id])
GO
ALTER TABLE [dbo].[GroupEvaluation] CHECK CONSTRAINT [FK_GroupEvaluation_Evaluation]
GO
ALTER TABLE [dbo].[GroupEvaluation]  WITH CHECK ADD  CONSTRAINT [FK_GroupEvaluation_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupEvaluation] CHECK CONSTRAINT [FK_GroupEvaluation_Group]
GO
ALTER TABLE [dbo].[GroupProject]  WITH CHECK ADD  CONSTRAINT [FK_GroupProject_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupProject] CHECK CONSTRAINT [FK_GroupProject_Group]
GO
ALTER TABLE [dbo].[GroupProject]  WITH CHECK ADD  CONSTRAINT [FK_GroupProject_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[GroupProject] CHECK CONSTRAINT [FK_GroupProject_Project]
GO
ALTER TABLE [dbo].[GroupStudent]  WITH CHECK ADD  CONSTRAINT [FK_GroupStudents_Lookup] FOREIGN KEY([Status])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[GroupStudent] CHECK CONSTRAINT [FK_GroupStudents_Lookup]
GO
ALTER TABLE [dbo].[GroupStudent]  WITH CHECK ADD  CONSTRAINT [FK_ProjectStudents_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupStudent] CHECK CONSTRAINT [FK_ProjectStudents_Group]
GO
ALTER TABLE [dbo].[GroupStudent]  WITH CHECK ADD  CONSTRAINT [FK_ProjectStudents_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[GroupStudent] CHECK CONSTRAINT [FK_ProjectStudents_Student]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Lookup] FOREIGN KEY([Gender])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Lookup]
GO
ALTER TABLE [dbo].[ProjectAdvisor]  WITH CHECK ADD  CONSTRAINT [FK_ProjectAdvisor_Lookup] FOREIGN KEY([AdvisorRole])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdvisor] CHECK CONSTRAINT [FK_ProjectAdvisor_Lookup]
GO
ALTER TABLE [dbo].[ProjectAdvisor]  WITH CHECK ADD  CONSTRAINT [FK_ProjectAdvisor_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdvisor] CHECK CONSTRAINT [FK_ProjectAdvisor_Project]
GO
ALTER TABLE [dbo].[ProjectAdvisor]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTeachers_Teacher] FOREIGN KEY([AdvisorId])
REFERENCES [dbo].[Advisor] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdvisor] CHECK CONSTRAINT [FK_ProjectTeachers_Teacher]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Person] FOREIGN KEY([Id])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Person]
GO
