import json
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent

INPUT_FILE = ROOT / "problems" / "input.json"
PROBLEMS_DIR = ROOT / "problems"


# Читаем исходный JSON
with INPUT_FILE.open("r", encoding="utf-8") as file:
    problems = json.load(file)

created = 0

for problem in problems:
    problem_id = problem["id"]

    # Создаём JSON напрямую в папке problems
    output_file = PROBLEMS_DIR / f"{problem_id}.json"

    is_new = not output_file.exists()

    with output_file.open("w", encoding="utf-8") as file:
        json.dump(problem, file, ensure_ascii=False, indent=2)

    if is_new:
        created += 1

print(f"Created {created} new JSON files.")