using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameTarget : Minigame
{
    [SerializeField] private MovingTarget movingTargetPrefab;
    [SerializeField] private ParticleSystem particlePrefab;

    private int targetsHit;
    private bool escalated;

    private List<MovingTarget> allTargets = new();
    private const int MAX_SCORE = 99;

    public override void StartMinigame()
    {
        base.StartMinigame();
        targetsHit = 0;
        escalated = false;
        CleanAllTargets();
        SpawnTargets(Random.Range(5, 9));
    }

    private void CleanAllTargets()
    {
        foreach (var target in allTargets)
            if (target != null)
                Destroy(target.gameObject);

        allTargets.Clear();
    }

    public override void OnUpdate(float deltaTime)
    {
       // HandleEscalation();
        HandleWinCondition();
    }

    // TODO: How will the escalation be triggered?
    private void HandleEscalation()
    {
        if (!escalated)
        {
            escalated = true;
            SpawnTargets(3);

            foreach (var target in allTargets)
                target.Speed *= 1.5f;
        }
    }

    private void HandleWinCondition()
    {
        if (!allTargets.Exists(x => x.ShouldBeHit))
        {
            CleanAllTargets();
            Finish(true);
        }
    }

    public void HitTarget(MovingTarget target, bool shouldBeHit, PointerEventData eventData)
    {
        if (target == null)
            return;

        targetsHit = Mathf.Clamp(targetsHit + (shouldBeHit ? 1 : -1), 0, MAX_SCORE);

        if (shouldBeHit)
        {
            var pos = Camera.main.ScreenToWorldPoint(eventData.position);
            var particle = Instantiate(particlePrefab);
            particle.transform.position = pos;
            particle.Play();
            allTargets.Remove(target);
            Destroy(target.gameObject);
        }
    }

    private void SpawnTargets(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var target = Instantiate(movingTargetPrefab);
            target.Initialize(this);
            allTargets.Add(target);
        }
    }
}