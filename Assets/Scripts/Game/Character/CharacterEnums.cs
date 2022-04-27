public enum CharacterConditions : byte
{
    Invulrnerable = 1,
    Normal = 2,
    Disabled = 3,
    Dead = 4,
    Damaged = 5
}

public enum MovementStates : byte
{
    Idle = 1,
    Walking = 2,
    Dashing = 3,
    Attacking = 4,
    Ultimate = 5
}

public enum CharacterType
{
    Master,
    Ally,
    Enemy
}