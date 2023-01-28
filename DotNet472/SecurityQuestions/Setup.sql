CREATE  DATABASE  SecurityQuestions;
GO
CREATE TABLE SecurityQuestions.dbo.Accounts (
    AccountId INT IDENTITY(1,1) PRIMARY KEY,
    AccountName VARCHAR(MAX)
    );
GO
CREATE TABLE SecurityQuestions.dbo.Questions (
    QuestionId INT IDENTITY PRIMARY KEY,
    QuestionString VARCHAR(MAX)
    );
GO
CREATE TABLE SecurityQuestions.dbo.AccountQuestions (
    AccountId INT,
    QuestionId INT,
    Answer VARCHAR(MAX),
    CONSTRAINT AccountQuestions_PK PRIMARY KEY (AccountId, QuestionId),
    CONSTRAINT Account_FK FOREIGN KEY (AccountId) REFERENCES SecurityQuestions.dbo.Accounts (AccountId),
    CONSTRAINT Question_FK FOREIGN KEY (QuestionId) REFERENCES SecurityQuestions.dbo.Questions (QuestionId)
);
GO
INSERT INTO SecurityQuestions.dbo.Questions
(QuestionString)
VALUES
('In what city were you born?'),
('What is the name of your favorite pet?'),
('What is your mother''s maiden name?'),
('What high school did you attend?'),
('What was the mascot of your high school?'),
('What was the make of your first car?'),
('What was your favorite toy as a child?'),
('Where did you meet your spouse?'),
('Who is your favorite actor/actress?'),
('What is your favorite album?')
GO