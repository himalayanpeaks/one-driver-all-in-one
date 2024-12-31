using Framework.Libs.Validator;
using Moq;
using PowerSupply.General.Products;

namespace PowerSupply.General.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test_DeviceInitialization()
        {
            // Arrange
            var mockValidator = new Mock<IValidator>();
            var mockPowerSupplyHAL = new Mock<IPowerSupplyHAL>();
            mockPowerSupplyHAL.Setup(hal => hal.NumberOfChannels).Returns(2);

            // Act
            var device = new Device("TestDevice", mockValidator.Object, mockPowerSupplyHAL.Object);

            // Assert
            Assert.Equal("TestDevice", device.Parameters.Name);
            Assert.Equal(2, device.Elements.Count); // 1 initial channel + 2 from NumberOfChannels
            Assert.Equal("Ch0", device.Elements[0].Parameters.Name);
            Assert.Equal("Ch1", device.Elements[1].Parameters.Name);

        }
    }
}