using StorageService.Business.Commands.File.Create;
using Xunit;

namespace Tests.Business.Files.Validators
{
    public class CreateFileValidatorTests
    {
        [Fact]
        public void Validate_ValidRequest()
        {
            var validator = new CreateFileCommandValidator();

            var query1 = new CreateFileCommand("Test", null);
            var result1 = validator.Validate(query1);
            Assert.True(result1.IsValid);
        }

        [Fact]
        public void Validate_NameIsEmptyString_Invalid()
        {
            var validator = new CreateFileCommandValidator();

            var query = new CreateFileCommand(string.Empty, null);
            var result = validator.Validate(query);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_NameIsWhitespaceString_Invalid()
        {
            var validator = new CreateFileCommandValidator();

            var query = new CreateFileCommand(" " ,null);
            var result = validator.Validate(query);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_NameIsNullString_Invalid()
        {
            var validator = new CreateFileCommandValidator();

            var query = new CreateFileCommand(null, null);
            var result = validator.Validate(query);
            Assert.False(result.IsValid);
        }
    }
}
