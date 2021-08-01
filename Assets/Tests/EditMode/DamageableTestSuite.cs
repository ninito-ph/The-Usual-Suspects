using FluentAssertions;
using Ninito.UsualSuspects.Damage;
using NSubstitute;
using NUnit.Framework;

namespace Ninito.UsualSuspects.Tests.Damageable
{
    public class DamageableTestSuite
    {
        public class DamageableDamageTests
        {
            private Damage.Damageable _damageable;
            private Type _damageType;

            [SetUp]
            public void SetupDamageTests()
            {
                _damageable = new Damage.Damageable(Damage.Damageable.DamageableMode.Depletion, 100);
                _damageType = Substitute.For<Type>();
            }

            [Test]
            public void Damage_OnUndamagedDamageable_TakesDamage()
            {
                // Act
                _damageable.Damage(1, _damageType);

                // Assert
                _damageable.Health.Should().Be(99);
            }

            [Test]
            public void Damage_EnoughToKill_Kills()
            {
                // Act
                bool damageableCalledDied = false;
                _damageable.Died += () => { damageableCalledDied = true; };
                _damageable.Damage(100, _damageType);

                // Assert
                damageableCalledDied.Should().BeTrue();
            }

            [Test]
            public void Damage_WithResistantDamageType_DamagesHalf()
            {
                // Arrange
                _damageable.ResistantDamageTypes.Add(_damageType);

                // Act
                _damageable.Damage(2, _damageType);

                // Assert
                _damageable.Health.Should().Be(99);
            }

            [Test]
            public void Damage_WithVulnerableDamageType_DamagesDouble()
            {
                // Arrange
                _damageable.VulnerableDamageTypes.Add(_damageType);

                // Act
                _damageable.Damage(1, _damageType);

                // Assert
                _damageable.Health.Should().Be(98);
            }

            [Test]
            public void Damage_WithImmuneDamageType_DoesNotDamage()
            {
                // Arrange
                _damageable.ImmuneDamageTypes.Add(_damageType);

                // Act
                _damageable.Damage(1, _damageType);

                // Assert
                _damageable.Health.Should().Be(100);
            }
        }

        public class HealDamageTests
        {
            private Damage.Damageable _damageable;
            private Type _damageType;

            [SetUp]
            public void SetupDamageTests()
            {
                _damageable = new Damage.Damageable(Damage.Damageable.DamageableMode.Depletion, 100);
                _damageType = Substitute.For<Type>();
            }

            [Test]
            public void Heal_WithFullHealth_DoesNotExceedMaxHealth()
            {
                // Act
                _damageable.Heal(1);

                // Assert
                _damageable.Health.Should().Be(100);
            }
            
            [Test]
            public void Heal_WithIncompleteHealth_Heals()
            {
                _damageable.Damage(1, _damageType);
                
                // Act
                _damageable.Heal(1);

                // Assert
                _damageable.Health.Should().Be(100);
            }
        }
    }
}