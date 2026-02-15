using AutoMapper;
using FluentAssertions;
using MovieLibrary.Business.Repositories.Interfaces;
using MovieLibrary.Business.Services.Implementations;
using MovieLibrary.Models.Domain.Entities;
using MovieLibrary.Models.ViewModels.Actors;
using NSubstitute;
using Xunit;

namespace MovieLibrary.Tests.Services
{
    public class ActorServiceTests
    {
        private readonly IRepository<Actor> _repository;
        private readonly IMapper _mapper;
        private readonly ActorService _service;

        public ActorServiceTests()
        {
            _repository = Substitute.For<IRepository<Actor>>();
            _mapper = Substitute.For<IMapper>();

            _service = new ActorService(_repository, _mapper);
        }

        #region CreateAsync

        [Fact]
        public async Task CreateAsync_Should_Add_Actor_And_Return_Id()
        {
            // Arrange
            var model = new ActorCreateOrEditViewModel
            {
                FullName = "Keanu Reeves",
                BirthDate = new DateTime(1964, 9, 2)
            };

            var mappedActor = new Actor
            {
                FullName = model.FullName,
                BirthDate = model.BirthDate
            };

            _mapper.Map<Actor>(model).Returns(mappedActor);

            // Act
            var result = await _service.CreateAsync(model);

            // Assert
            await _repository.Received(1).AddAsync(mappedActor);
            await _repository.Received(1).CommitAsync();

            result.Should().NotBe(Guid.Empty);
        }

        #endregion

        #region UpdateAsync

        [Fact]
        public async Task UpdateAsync_Should_Return_False_When_Actor_Not_Found()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repository.GetByIdAsync(id).Returns((Actor?)null);

            var model = new ActorCreateOrEditViewModel();

            // Act
            var result = await _service.UpdateAsync(id, model);

            // Assert
            result.Should().BeFalse();
            _repository.DidNotReceive().Update(Arg.Any<Actor>());
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Actor_When_Found()
        {
            // Arrange
            var id = Guid.NewGuid();

            var existingActor = new Actor
            {
                Id = id,
                FullName = "Old Name"
            };

            var model = new ActorCreateOrEditViewModel
            {
                FullName = "New Name"
            };

            _repository.GetByIdAsync(id).Returns(existingActor);

            // Act
            var result = await _service.UpdateAsync(id, model);

            // Assert
            result.Should().BeTrue();

            _mapper.Received(1).Map(model, existingActor);
            _repository.Received(1).Update(existingActor);
            await _repository.Received(1).CommitAsync();
        }

        #endregion

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_Should_Return_False_When_Not_Found()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repository.GetByIdAsync(id).Returns((Actor?)null);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            result.Should().BeFalse();
            _repository.DidNotReceive().Remove(Arg.Any<Actor>());
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Actor_When_Found()
        {
            // Arrange
            var id = Guid.NewGuid();
            var actor = new Actor { Id = id };

            _repository.GetByIdAsync(id).Returns(actor);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            result.Should().BeTrue();
            _repository.Received(1).Remove(actor);
            await _repository.Received(1).CommitAsync();
        }

        #endregion

        #region GetForEditAsync

        [Fact]
        public async Task GetForEditAsync_Should_Return_Null_When_Not_Found()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repository.GetByIdAsync(id).Returns((Actor?)null);

            // Act
            var result = await _service.GetForEditAsync(id);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetForEditAsync_Should_Map_Actor_When_Found()
        {
            // Arrange
            var id = Guid.NewGuid();

            var actor = new Actor { Id = id, FullName = "Test" };

            var viewModel = new ActorCreateOrEditViewModel
            {
                Id = id,
                FullName = "Test"
            };

            _repository.GetByIdAsync(id).Returns(actor);
            _mapper.Map<ActorCreateOrEditViewModel>(actor).Returns(viewModel);

            // Act
            var result = await _service.GetForEditAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(viewModel);
        }

        #endregion
    }
}
