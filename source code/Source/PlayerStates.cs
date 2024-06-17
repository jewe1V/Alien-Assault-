namespace Game
{
    public class PlayerStates
    {
        public bool IsRight;
        public bool IsLeft;
        public bool IsIdle;
        public bool IsMoving;
        public bool IsRunning;
        public bool IsShooting;
        public bool IsReloading;
        public bool IsHurting;

        public void SetRight()
        {
            ResetSides();
            IsRight = true;
        }

        public void SetLeft()
        {
            ResetSides();
            IsLeft = true;
        }
        public void SetIdle()
        {
            ResetStates();
            IsIdle = true;
        }

        public void SetMoving()
        {
            ResetStates();
            IsMoving = true;
        }

        public void SetRunning()
        {
            ResetStates();
            IsRunning = true;
        }

        public void SetShooting()
        {
            ResetStates();
            IsShooting = true;
        }

        public void SetReloading()
        {
            ResetStates();
            IsReloading = true;
        }

        public void SetHurting()
        {
            ResetStates();
            IsHurting = true;
        }

        private void ResetStates()
        {
            IsIdle = false;
            IsMoving = false;
            IsRunning = false;
            IsHurting = false;
        }

        private void ResetSides()
        {
            IsLeft = false;
            IsRight = false;
        }
    }
}
