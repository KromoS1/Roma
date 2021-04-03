using System;
using FluentAssertions;
using MoreLinq;
using Todo.CliArguments.Exceptions;
using Xunit;
using static Todo.CliArguments.CommandDefinitionBuilder;
using static Todo.CliArguments.ArgumentDefinitionBuilder;

namespace Todo.CliArguments
{
    public class CliCommandParserTests
    {
        [Fact]
        public void Parse_WhenArgumentsAreEmpty_ShouldThrowException()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            Func<CliCommand> act = () => sut.Parse();
            
            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void Parse_WhenDontMatchAnyCommand_ShouldThrowException()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            Func<CliCommand> act = () => sut.Parse("notFound");
            
            // Assert
            act.Should()
               .ThrowExactly<CommandNotFoundException>().And
               .ExpectedCommandName.Should().Be("notFound");
        }
        
        [Fact]
        public void Parse_WhenMatchArgumentlessCommand_ShouldParse()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            var command = sut.Parse("list");
            
            // Assert
            command.Name.Should().Be("list");
        }

        [Fact]
        public void Parse_WhenMatchArgumentsByLongNameWithoutValue_ShouldFillArguments()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            var command = sut.Parse("trim", "--output");
            
            // Assert
            command.Name.Should().Be("trim");
            command.HasArgument("output").Should().BeTrue();
        }
        
        [Fact]
        public void Parse_WhenMatchArgumentsByShortNameWithoutValue_ShouldFillArguments()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            var command = sut.Parse("trim", "-o");
            
            // Assert
            command.Name.Should().Be("trim");
            command.HasArgument("output").Should().BeTrue();
        }
        
        [Fact]
        public void Parse_WhenDontMatchAnyArgumentsByLongName_ShouldThrowException()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            Func<CliCommand> act = () => sut.Parse("trim", "--not-existed");
            
            // Assert
            var exception = act.Should().ThrowExactly<ArgumentNotFoundException>().Which;
            exception.Name.Should().Be("not-existed");
            exception.IsShort.Should().BeFalse();
        }
        
        [Fact]
        public void Parse_WhenMatchArgumentsByLongName_ShouldFillArguments()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            var command = sut.Parse("trim", "--size", "10");
            
            // Assert
            command.Name.Should().Be("trim");
            command.GetArgument("size").Should().Be("10");
        }
        
        [Fact]
        public void Parse_WhenArgumentWithExpectedValueFollowWithKey_ShouldThrowException()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            Func<CliCommand> act = () => sut.Parse("trim", "--size", "--output");
            
            // Assert
            act.Should()
               .ThrowExactly<ExpectedValueException>().And
               .ArgumentName.Should().Be("size");
        }
        
        [Fact]
        public void Parse_WhenArgumentWithExpectedValueLeftUnfilled_ShouldThrowException()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            Func<CliCommand> act = () => sut.Parse("trim", "--size");
            
            // Assert
            act.Should()
               .ThrowExactly<ExpectedValueException>().And
               .ArgumentName.Should().Be("size");
        }
        
        [Fact]
        public void Parse_WhenUnexpectedValueInsideCommand_ShouldThrowException()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            Func<CliCommand> act = () => sut.Parse("trim", "--size", "10", "something", "-o");
            
            // Assert
            act.Should()
               .ThrowExactly<ExpectedKeyException>().And
               .FoundToken.Should().Be("something");
        }
        
        [Fact]
        public void Parse_WhenUnexpectedSubject_ShouldThrowException()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            Func<CliCommand> act = () => sut.Parse("trim", "unexpected-subject");
            
            // Assert
            act.Should()
               .ThrowExactly<NotExpectedSubjectException>().And
               .FoundToken.Should().Be("unexpected-subject");
        }
        
        [Fact]
        public void Parse_WhenMatchCommandWithSubject_ShouldFillSubject()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            var command = sut.Parse("add", "-p", "10", "item-name");
            
            // Assert
            command.Name.Should().Be("add");
            command.Subject.Should().Be("item-name");
        }
        
        [Fact]
        public void Parse_WhenNotFoundExpectedSubject_ShouldThrowException()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            Func<CliCommand> act = () => sut.Parse("add", "-p", "10");
            
            // Assert
            act.Should()
               .ThrowExactly<ExpectedSubjectException>();
        }
        
        [Fact]
        public void Parse_WhenMultipleArgumentsProvided_ShouldFillThemAll()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            var command = sut.Parse("add", "-p", "10", "-o", "--due", "tomorrow", "Code review");
            
            // Assert
            command.Name.Should().Be("add");
            command.GetArgument("priority").Should().Be("10");
            command.GetArgument("due").Should().Be("tomorrow");
            command.HasArgument("output").Should().BeTrue();
            command.Subject.Should().Be("Code review");
        }

        [Fact]
        public void Parse_WhenArgumentValueDoesntPassValidation_ShouldThrowException()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            Func<CliCommand> act = () => sut.Parse("add", "-p", "ten", "hello");
            
            // Assert
            var exception = act.Should().ThrowExactly<ArgumentValidationException>().Which;
            exception.ArgumentName.Should().Be("priority");
            exception.ActualValue.Should().Be("ten");
        }
        
        [Fact]
        public void Parse_WhenSubjectDoesntPassValidation_ShouldThrowException()
        {
            // Arrange
            var sut = GetCliCommandParser();
            
            // Act
            Func<CliCommand> act = () => sut.Parse("add", "hi");
            
            // Assert
            act.Should()
               .ThrowExactly<SubjectValidationException>().And
               .ActualSubject.Should().Be("hi");
        }
        
        private CliCommandParser GetCliCommandParser()
        {
            var commands = new[]
            {
                NewCommand("list")
                   .WithHelpText("Shows all elements")
                   .Build(),
                
                NewCommand("trim")
                   .WithHelpText("Trims your collection to defined size")
                   .WithArgument(
                        NewArgument("size")
                           .WithShortName("s")
                           .WithValueExpected(x => int.TryParse(x, out _))
                           .Build())
                   .WithArgument(
                        NewArgument("output")
                           .WithShortName("o")
                           .WithHelpText("Shows list of items after trimming").
                            Build())
                   .Build(),
                
                NewCommand("add")
                   .WithHelpText("Adds new element")
                   .WithArgument(
                        NewArgument("output")
                           .WithShortName("o")
                           .WithHelpText("Shows list of items after adding").
                            Build())
                   .WithArgument(
                        NewArgument("due")
                           .WithHelpText("Sets deadline for item")
                           .WithValueExpected(x => !string.IsNullOrWhiteSpace(x))
                           .Build())
                   .WithArgument(
                        NewArgument("priority")
                           .WithShortName("p")
                           .WithHelpText("Priority of added element")
                           .WithValueExpected(x => int.TryParse(x, out _))
                           .Build())
                   .WithExpectedSubject(x => !string.IsNullOrWhiteSpace(x) && x.Length >= 5)
                   .Build()
            };

            var parser = new CliCommandParser();

            commands.ForEach(parser.AddCommandDefinition);
            
            return parser;
        }
    }
}