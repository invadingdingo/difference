using UnityEngine;
using System.Collections.Generic;

// Written by Steve Streeting 2017
// License: CC0 Public Domain http://creativecommons.org/publicdomain/zero/1.0/

/// <summary>
/// Component which will flicker a linked light while active by changing its
/// intensity between the min and max values given. The flickering can be
/// sharp or smoothed depending on the value of the smoothing parameter.
///
/// Just activate / deactivate this component as usual to pause / resume flicker
/// </summary>
public class Lightning : MonoBehaviour {
    [Tooltip("External light to flicker; you can leave this null if you attach script to a light")]
    public GameObject source;

    [Tooltip("Min Cooldown (In seconds)")]
    public float minCooldown;
    [Tooltip("Max Cooldown (In seconds)")]
    public float maxCooldown;
    [Tooltip("Chance for lightning to occur")]
    public float lightningChance;
    public float flashTime;

    private float currentTimer;
    private float flashTimer;

    void Update() {
        if ((currentTimer > minCooldown) && (currentTimer < maxCooldown)) {
            if (Random.Range(0.0f, 1.0f) < lightningChance) {
                Flash();
            }
        } else if (currentTimer > maxCooldown) {
            Flash();
        }

        currentTimer += Time.deltaTime;
    }

    void Flash() {
        source.SetActive(true);

        if (flashTimer >= flashTime) {
            source.SetActive(false);
            flashTimer = 0;
            currentTimer = 0;
        } 

        flashTimer += Time.deltaTime;
    }

}