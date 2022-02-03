namespace Orchestration.GetCompetetorsForIrp;

public class GetCompetetorsForIrpDto
{
	public List<IrpCompetetorDto> FasterAthletes { get; }

	public List<IrpCompetetorDto> SlowerAthletes { get; }

	public GetCompetetorsForIrpDto(List<IrpCompetetorDto> fasterAthletes, List<IrpCompetetorDto> slowerAthletes)
	{
		FasterAthletes = fasterAthletes;
		SlowerAthletes = slowerAthletes;
	}
}
