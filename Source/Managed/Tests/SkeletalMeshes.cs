using System;
using System.Drawing;
using System.Numerics;
using System.Reflection;
using UnrealEngine.Framework;

namespace UnrealEngine.Tests {
	public static class SkeletalMeshes {
		public static void OnBeginPlay() {
			Debug.AddOnScreenMessage(-1, 3.0f, Color.LightGreen, MethodBase.GetCurrentMethod().DeclaringType + " system started!");

			World.GetFirstPlayerController().SetViewTarget(World.GetActor<Camera>("MainCamera"));

			SkeletalMesh prototypeMesh = SkeletalMesh.Load("/Game/Tests/Characters/Prototype");

			Actor leftPrototype = new Actor("leftPrototype");
			SkeletalMeshComponent leftSkeletalMeshComponent = new SkeletalMeshComponent(leftPrototype);

			leftSkeletalMeshComponent.SetSkeletalMesh(prototypeMesh);
			leftSkeletalMeshComponent.SetWorldLocation(new Vector3(-700.0f, -70.0f, -100.0f));
			leftSkeletalMeshComponent.SetWorldRotation(Maths.Euler(0.0f, 0.0f, 90.0f));
			leftSkeletalMeshComponent.SetAnimationMode(AnimationMode.Asset);
			leftSkeletalMeshComponent.PlayAnimation(AnimationSequence.Load("/Game/Tests/Characters/Animations/IdleAnimationSequence"), true);

			Assert.IsTrue(leftSkeletalMeshComponent.IsPlaying);

			Actor rightPrototype = new Actor("rightPrototype");
			SkeletalMeshComponent rightSkeletalMeshComponent = new SkeletalMeshComponent(rightPrototype);

			rightSkeletalMeshComponent.SetSkeletalMesh(prototypeMesh);
			rightSkeletalMeshComponent.SetWorldLocation(new Vector3(-700.0f, 70.0f, -100.0f));
			rightSkeletalMeshComponent.SetWorldRotation(Maths.Euler(0.0f, 0.0f, 90.0f));
			rightSkeletalMeshComponent.SetAnimationMode(AnimationMode.Asset);
			rightSkeletalMeshComponent.CreateAndSetMaterialInstanceDynamic(0).SetVectorParameterValue("AccentColor", new LinearColor(0.0f, 0.5f, 1.0f));

			AnimationMontage rightPrototypeAnimationMontage = AnimationMontage.Load("/Game/Tests/Characters/Animations/RunAnimationMontage");

			rightSkeletalMeshComponent.PlayAnimation(rightPrototypeAnimationMontage, true);

			AnimationInstance rightPrototypeAnimationInstance = rightSkeletalMeshComponent.GetAnimationInstance();

			Assert.IsTrue(rightPrototypeAnimationInstance.IsPlaying(rightPrototypeAnimationMontage));
		}

		public static void OnEndPlay() => Debug.ClearOnScreenMessages();
	}
}