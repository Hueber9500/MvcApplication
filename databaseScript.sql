USE [PresentsDB]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/23/2016 23:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Pass] [varchar](max) NOT NULL,
	[BirthDate] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Presents]    Script Date: 07/23/2016 23:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Presents](
	[PresentID] [int] IDENTITY(1,1) NOT NULL,
	[PresentName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PresentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vote]    Script Date: 07/23/2016 23:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vote](
	[UserID] [int] NOT NULL,
	[PresentID] [int] NULL,
	[AdminID] [int] NOT NULL,
	[BirthdayUserID] [int] NOT NULL,
	[IsStoppedVoting] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Default [DF__Vote__IsStoppedV__1B0907CE]    Script Date: 07/23/2016 23:03:43 ******/
ALTER TABLE [dbo].[Vote] ADD  DEFAULT ((0)) FOR [IsStoppedVoting]
GO
/****** Object:  ForeignKey [FK__Vote__AdminID__15502E78]    Script Date: 07/23/2016 23:03:43 ******/
ALTER TABLE [dbo].[Vote]  WITH CHECK ADD FOREIGN KEY([AdminID])
REFERENCES [dbo].[Users] ([UserID])
GO
/****** Object:  ForeignKey [FK__Vote__BirthdayUs__164452B1]    Script Date: 07/23/2016 23:03:43 ******/
ALTER TABLE [dbo].[Vote]  WITH CHECK ADD FOREIGN KEY([BirthdayUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
/****** Object:  ForeignKey [FK__Vote__PresentID__1367E606]    Script Date: 07/23/2016 23:03:43 ******/
ALTER TABLE [dbo].[Vote]  WITH CHECK ADD FOREIGN KEY([PresentID])
REFERENCES [dbo].[Presents] ([PresentID])
GO
/****** Object:  ForeignKey [FK__Vote__UserID__1273C1CD]    Script Date: 07/23/2016 23:03:43 ******/
ALTER TABLE [dbo].[Vote]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
