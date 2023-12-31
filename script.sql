USE [ZaloOAApi]
GO
/****** Object:  Table [dbo].[Scopes]    Script Date: 8/15/2023 8:32:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scopes](
	[ScopeId] [int] IDENTITY(1,1) NOT NULL,
	[ScopeName] [nvarchar](225) NULL,
PRIMARY KEY CLUSTERED 
(
	[ScopeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScopeTags]    Script Date: 8/15/2023 8:32:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScopeTags](
	[ScopeTagId] [int] IDENTITY(1,1) NOT NULL,
	[TagId] [int] NULL,
	[ScopeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ScopeTagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 8/15/2023 8:32:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[TagId] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](225) NULL,
PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 8/15/2023 8:32:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserId] [bigint] NOT NULL,
	[UserGender] [int] NULL,
	[UserAppId] [bigint] NULL,
	[IsSensitive] [bit] NULL,
	[DisplayName] [nvarchar](225) NULL,
	[BirthDate] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTags]    Script Date: 8/15/2023 8:32:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTags](
	[UserTagId] [int] IDENTITY(1,1) NOT NULL,
	[TagId] [int] NULL,
	[UserId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserTagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Scopes] ON 

INSERT [dbo].[Scopes] ([ScopeId], [ScopeName]) VALUES (1, N'Calling')
INSERT [dbo].[Scopes] ([ScopeId], [ScopeName]) VALUES (2, N'Production')
SET IDENTITY_INSERT [dbo].[Scopes] OFF
GO
SET IDENTITY_INSERT [dbo].[ScopeTags] ON 

INSERT [dbo].[ScopeTags] ([ScopeTagId], [TagId], [ScopeId]) VALUES (5, 2, 2)
INSERT [dbo].[ScopeTags] ([ScopeTagId], [TagId], [ScopeId]) VALUES (6, 2, 1)
SET IDENTITY_INSERT [dbo].[ScopeTags] OFF
GO
SET IDENTITY_INSERT [dbo].[Tags] ON 

INSERT [dbo].[Tags] ([TagId], [TagName]) VALUES (2, N'Daco')
INSERT [dbo].[Tags] ([TagId], [TagName]) VALUES (7, N'Hoàn thành')
INSERT [dbo].[Tags] ([TagId], [TagName]) VALUES (8, N'Calling')
INSERT [dbo].[Tags] ([TagId], [TagName]) VALUES (9, N'TagNameTest')
SET IDENTITY_INSERT [dbo].[Tags] OFF
GO
INSERT [dbo].[UserProfile] ([UserId], [UserGender], [UserAppId], [IsSensitive], [DisplayName], [BirthDate]) VALUES (102318638252200582, 0, 0, 0, N'Huy', N'0')
INSERT [dbo].[UserProfile] ([UserId], [UserGender], [UserAppId], [IsSensitive], [DisplayName], [BirthDate]) VALUES (872118638252200582, 1, 0, 1, N'Tú', N'0')
GO
SET IDENTITY_INSERT [dbo].[UserTags] ON 

INSERT [dbo].[UserTags] ([UserTagId], [TagId], [UserId]) VALUES (2, 2, 872118638252200582)
INSERT [dbo].[UserTags] ([UserTagId], [TagId], [UserId]) VALUES (10, 2, 102318638252200582)
SET IDENTITY_INSERT [dbo].[UserTags] OFF
GO
ALTER TABLE [dbo].[ScopeTags]  WITH CHECK ADD FOREIGN KEY([ScopeId])
REFERENCES [dbo].[Scopes] ([ScopeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScopeTags]  WITH CHECK ADD FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([TagId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTags]  WITH CHECK ADD FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([TagId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTags]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
ON DELETE CASCADE
GO
