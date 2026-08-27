public enum LockonMode
{
    /*
    Auto 상태:
    범위 내 대상이 있으면 예측 위치 = 대상 위치(또는 리드 예측 위치).
    대상이 없으면 Manual처럼 조준 입력으로 예측 위치를 자유롭게 움직일 수 있고,
    그 상태에서 범위 내에 후보가 다시 나타나면 가장 가까운 대상으로 자동 락온한다.
    */
    Auto,
    // - Manual 상태: ApplyManualAimDelta로 예측 위치를 직접 이동. 대상이 있어도 무시한다.
    Manual, 
}
