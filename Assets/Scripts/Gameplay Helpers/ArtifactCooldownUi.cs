using UnityEngine;
using UnityEngine.UI;
public class ArtifactCooldownUi : MonoBehaviour
{
    public ArtifactAbility artifactAbility;
    public Image cooldownOverlay;

    private void Update()
    {
        if (cooldownOverlay == null || artifactAbility == null) return;

        float remaining = artifactAbility.cooldownTimer - Time.time;

        if (remaining > 0)
            cooldownOverlay.fillAmount = remaining / artifactAbility.cooldown;
        else
            cooldownOverlay.fillAmount = 0f;
    }
}
