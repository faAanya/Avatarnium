using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Combat Values")]
		public bool fire1;
		public bool fire2;
		public bool fire3;
		public bool fire4;
		public bool fire5;
		public bool isLight;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool aim;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);


		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		#region Combat
		public void OnFire1(InputValue value)
		{
			Fire1Input(value.isPressed);
		}

		private void Fire1Input(bool isPressed)
		{
			fire1 = isPressed;
		}

		public void OnFire2(InputValue value)
		{
			Fire2Input(value.isPressed);
		}

		private void Fire2Input(bool isPressed)
		{
			fire2 = isPressed;
		}

		public void OnFire3(InputValue value)
		{
			Fire3Input(value.isPressed);
		}

		private void Fire3Input(bool isPressed)
		{
			fire3 = isPressed;
		}

		public void OnFire4(InputValue value)
		{
			Fire4Input(value.isPressed);
		}

		private void Fire4Input(bool isPressed)
		{
			fire4 = isPressed;
		}

		public void OnFire5(InputValue value)
		{
			Fire5Input(value.isPressed);
		}

		private void Fire5Input(bool isPressed)
		{
			fire5 = isPressed;
		}
		public void LightInput(bool newLightState)
		{
			isLight = newLightState;
		}
		public void OnLight(InputValue value)
		{
			LightInput(value.isPressed);
		}
		#endregion

#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}



		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}


		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}

}