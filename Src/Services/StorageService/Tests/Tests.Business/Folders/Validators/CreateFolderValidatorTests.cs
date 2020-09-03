using StorageService.Business.Commands.Folder.Create;
using Xunit;

namespace Tests.Business.Folders.Validators
{
    public class CreateFolderValidatorTests
    {
        [Fact]
        public void Validate_ValidRequest()
        {
            var validator = new CreateFolderCommandValidator();

            var query1 = new CreateFolderCommand("Test", null);
            var result1 = validator.Validate(query1);
            Assert.True(result1.IsValid);
        }

        [Fact]
        public void Validate_NameIsEmptyString_Invalid()
        {
            var validator = new CreateFolderCommandValidator();

            var query = new CreateFolderCommand(string.Empty, null);
            var result = validator.Validate(query);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_NameIsWhitespaceString_Invalid()
        {
            var validator = new CreateFolderCommandValidator();

            var query = new CreateFolderCommand(" ", null);
            var result = validator.Validate(query);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_NameIsNullString_Invalid()
        {
            var validator = new CreateFolderCommandValidator();

            var query = new CreateFolderCommand(null, null);
            var result = validator.Validate(query);
            Assert.False(result.IsValid);
        }
    }
}
